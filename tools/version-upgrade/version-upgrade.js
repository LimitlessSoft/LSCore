const fs = require('fs');
const { XMLParser, XMLBuilder } = require('fast-xml-parser');
const { execFile } = require('child_process');
const path = require('path');

const xmlOptions = {
  ignoreAttributes: false,
  format: true,
  unpairedTags: ['PackageReference', 'ProjectReference'],
  suppressUnpairedNode: false,
};

let config = {};

const args = process.argv.slice(2);

const findAllCsProjs = (dir, fileList = []) => {
  const entries = fs.readdirSync(dir, { withFileTypes: true });

  for (let entry of entries) {
    const fullPath = path.join(dir, entry.name);
    if (entry.isDirectory()) {
      findAllCsProjs(fullPath, fileList);
    } else if (entry.name.endsWith('.csproj')) {
      fileList.push(fullPath);
    }
  }
  return fileList;
};

fs.exists('./version-upgrade.config', (exists) => {
  if (exists) {
    config = JSON.parse(fs.readFileSync('./version-upgrade.config'));
  } else {
    config = {
      currentVersion: '0.0.1',
    };
    fs.writeFileSync('./version-upgrade.config', JSON.stringify(config));
  }

  const split = config.currentVersion.split('.');
  let currentPatch = parseInt(split[2]);
  let currentMinor = parseInt(split[1]);
  let currentMajor = parseInt(split[0]);

  // if args contains --upgrade-minor, upgrade minor version
  if (args.includes('--upgrade-minor')) {
    config.nextVersion = `${currentMajor}.${currentMinor + 1}.0`;
  } else {
    config.nextVersion = `${currentMajor}.${currentMinor}.${currentPatch + 1}`;
  }

  console.log('Config loaded!');
  console.log(config);
  console.log();

  const run = () => {
    const parser = new XMLParser(xmlOptions);

    const srcPath = '../../src/';

    const projects = fs
      .readdirSync(srcPath)
      .filter((x) => x.indexOf('LSCore') >= 0);

    console.log();
    console.log('Starting projects versions upgrade...');

    let allCsProjFiles = [];
    for (let i = 0; i < projects.length; i++) {
      const projectPath = path.join(srcPath, projects[i]);
      const csprojFiles = findAllCsProjs(projectPath);
      allCsProjFiles = allCsProjFiles.concat(csprojFiles);
    }

    for (let csprojFile of allCsProjFiles) {
      console.log(`Updating version on project: ${csprojFile}`);

      try {
        let csprojContent = fs.readFileSync(csprojFile, 'utf-8');
        let parsed = parser.parse(csprojContent);

        // Ensure PropertyGroup exists before trying to modify it
        if (!parsed.Project.PropertyGroup) {
          parsed.Project.PropertyGroup = {};
        }

        parsed.Project.PropertyGroup.Version = config.nextVersion;

        const builder = new XMLBuilder(xmlOptions);
        let xmlDataStr = builder.build(parsed);

        fs.writeFileSync(csprojFile, xmlDataStr);
      } catch (error) {
        console.error(`Error processing ${csprojFile}:`, error);
      }
    }

    fs.writeFileSync(
      './version-upgrade.config',
      JSON.stringify({ currentVersion: config.nextVersion })
    );

    const { execFile } = require('child_process');
    const child = execFile(
      `./version-upgrade.sh`,
      [config.nextVersion],
      (error, stdout, stderror) => {
        if (error) {
          throw error;
        }

        console.log();
        console.log(`Version successfully upgraded to ${config.nextVersion}!`);
      }
    );
  };

  run();
});