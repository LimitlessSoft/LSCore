const fs = require('fs')
const { XMLParser, XMLBuilder } = require('fast-xml-parser')

var config = {}

fs.exists('./version-upgrade.config', (e) => {
	if(e) {
		config = JSON.parse(fs.readFileSync('./version-upgrade.config'))
	} else {
		config = {
			currentVersion: 'v0.0.1'
		}
		fs.writeFileSync('./version-upgrade.config', JSON.stringify(config))
	}

	let patch = parseInt(config.currentVersion.substring(config.currentVersion.length - 1, config.currentVersion.length)) + 1
	config.nextVersion = config.currentVersion.substring(0, config.currentVersion.length - 1) + patch
	console.log("Config loaded!")
	console.log(config)
	console.log()

	run()
})


const run = () => {

	const parser = new XMLParser({ ignoreAttributes: false })

	const srcPath = '../src/'

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

		const options = {
			format: true,
			ignoreAttributes : false
		};

		const builder = new XMLBuilder(options);
		let xmlDataStr = builder.build(parsed);

		fs.writeFileSync('./version-upgrade.config', JSON.stringify({ currentVersion: config.currentVersion }))

		fs.writeFileSync(`${projectPath}/temp.csproj`, xmlDataStr)
	}
}
