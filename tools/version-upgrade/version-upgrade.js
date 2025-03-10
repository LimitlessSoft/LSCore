const fs = require('fs')
const { XMLParser, XMLBuilder } = require('fast-xml-parser')

const xmlOptions = {
	ignoreAttributes: false,
	format: true,
	unpairedTags: ["PackageReference", "ProjectReference"],
	suppressUnpairedNode: false,
}

var config = {}

var args = process.argv.slice(2)

fs.exists('./version-upgrade.config', (e) => {
	if(e) {
		config = JSON.parse(fs.readFileSync('./version-upgrade.config'))
	} else {
		config = {
			currentVersion: '0.0.1'
		}
		fs.writeFileSync('./version-upgrade.config', JSON.stringify(config))
	}
	
	let split = config.currentVersion.split('.')
	let currentPatch = parseInt(split[2])
	let currentMinor = parseInt(split[1])
	
	// if args contains --upgrade-minor, upgrade minor version
	if(args.indexOf('--upgrade-minor') >= 0)
		config.nextVersion = `${split[0]}.${currentMinor + 1}.0`
	else
		config.nextVersion = `${split[0]}.${currentMinor}.${currentPatch + 1}`
	
	console.log("Config loaded!")
	console.log(config)
	console.log()

	run()
})


const run = () => {

	const parser = new XMLParser(xmlOptions)

	const srcPath = '../../src/'

	const projects = fs.readdirSync(srcPath).filter(x => x.indexOf('LSCore') >= 0)

	console.log()
	console.log("Starting projects versions upgrade...")

	for(var i = 0; i < projects.length; i++) {
		console.log()
		console.log(`Updating version on project ${projects[i]}`)

		var projectPath = `${srcPath}${projects[i]}`
		let csprojFile = fs.readdirSync(`${projectPath}`).find(x => x.indexOf('.csproj') >= 0)
		let csprojContent = fs.readFileSync(`${projectPath}/${csprojFile}`)
		let parsed = parser.parse(csprojContent)

		parsed.Project.PropertyGroup.Version = config.nextVersion

		const builder = new XMLBuilder(xmlOptions);
		let xmlDataStr = builder.build(parsed);

		fs.writeFileSync(`${projectPath}/${csprojFile}`, xmlDataStr)
	}

	fs.writeFileSync('./version-upgrade.config', JSON.stringify({ currentVersion: config.nextVersion }))

	const { execFile } = require('child_process');
	const child = execFile(`./version-upgrade.sh`, [ config.nextVersion ], (error, stdout, stderror) => {
		if (error) {
			throw error;
		}

		console.log()
		console.log(`Version successfully upgraded to ${config.nextVersion}!`)
	});
}
