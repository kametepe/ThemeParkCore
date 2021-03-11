pipeline {
    agent any
    
     environment {
      
     
		
        SCRIPTS_DIR ="C:\\Jenkins\\tools\\scripts"  
        NUGET_DIR = "C:\\Jenkins\\tools\\nuget"
		DOTCOVER_DIR = "C:\\Jenkins\\tools\\dotcover"
		VSTEST_DIR = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow"
        ZAPCLI_DIR = "C:\\python\\Scripts"
		
		
		// This can be nexus3 or nexus2
        NEXUS_VERSION = "nexus3"
        // This can be http or https
        NEXUS_PROTOCOL = "http"
        // Where your Nexus is running
        NEXUS_URL = "localhost:8081"
        // Repository where we will upload the artifact
        NEXUS_REPOSITORY = "maven-releases"
        // Jenkins credential id to authenticate to Nexus OSS
        NEXUS_CREDENTIAL_ID = "nexus-credentails"
		
        // set the build Version
	    VERSION = "2.${java.time.LocalDate.now().getYear()}.${java.time.LocalDate.now().getDayOfYear().toString()}.${java.lang.String.format("%02d", java.time.LocalTime.now().getHour())}${java.lang.String.format("%02d", java.time.LocalTime.now().getMinute())}"
       //VERSION = "2.15.248.47"
      
    }


    stages { 
        stage ('Clean workspace') {
          steps {
              cleanWs()
           }
        }
        stage("CHECKOUT CODE") {
            steps {
                script {
                    // Let's clone the source

				    git url: 'https://github.com/kametepe/ThemeParkCore.git', branch: 'feature/netcore2.1', credentialsId: 'gitlab-default'
                }
            }
        }

      stage('RESTORE PACKAGES') {
            steps {
                sh 'dotnet restore'
            }
        }
        
          stage ('SCA : OWASP Dependency-Check Vulnerabilities') {
            steps {
                dependencyCheck additionalArguments: '', odcInstallation: 'OWASP-Dependency-Checker'
                dependencyCheckPublisher pattern: '**/dependency-check-report.xml'
            }
        }

stage('SAST : BUILD + UNIT TEST + CODE COVERAGE ')
      {
          
          
         steps {
             
             
              withSonarQubeEnv('SonarQube') {
       
sh 'dotnet test --logger "trx;LogFileName=TestResults.trx"  --results-directory BuildReports/UnitTests /p:CollectCoverage=true  /p:CoverletOutput=BuildReports/Coverage /p:CoverletOutputFormat=opencover  /p:Exclude="[xunit.*]*" '

sh 'dotnet build-server shutdown'
sh 'dotnet sonarscanner begin /k:THEME-PARK-CORE /d:sonar.host.url=http://localhost:9000 /d:sonar.login="6d847cd833a31520cc84ddf1293879ddbcde6a42" /d:sonar.cs.opencover.reportsPaths=ThemePark.Tests/BuildReports/Coverage.opencover.xml /d:sonar.coverage.exclusions=”**Test*.cs” '
sh 'dotnet build'
sh 'dotnet sonarscanner end /d:sonar.login="6d847cd833a31520cc84ddf1293879ddbcde6a42" '

sh 'reportgenerator -reports:ThemePark.Tests/BuildReports/Coverage.opencover.xml -targetdir:ThemePark.Tests/BuildReports/Coverage -reporttypes:"HTML;HTMLSummary"'

step([$class: 'MSTestPublisher', testResultsFile:"**/*.trx", failOnError: true, keepLongStdio: true])

publishHTML([allowMissing: false, alwaysLinkToLastBuild: false, keepAll: true, reportDir: "ThemePark.Tests/BuildReports/Coverage", reportFiles: 'index.html', reportName: "CodeCoverage Report"])

              }
         }
      }

       stage("SAST : QUALITY GATE") {
            steps {
              timeout(time: 1, unit: 'MINUTES') {
                waitForQualityGate abortPipeline: true
              }
                     
            }
          }

        
   
        
         stage('BUILD PACKAGE ') {
            steps {
                echo 'Perform Build'
                sh 'dotnet publish -c Release /p:configuration="release";platform="any cpu"'
            }
        }
        
         stage("ZIP ARTIFACTS ") {
            steps {
                script {
              zip archive: true, dir: 'ThemePark/bin/Release/netcoreapp2.1/publish/', glob: '', zipFile: "ThemePark-${VERSION}.zip"
                }
         
            }
          }

       stage('DAST') {
            steps {
                echo 'Perform DAST scanns'
            }
        }
        
      stage("PUBLISH TO NEXUS") {
            steps {
                script {
                    // Assign to a boolean response verifying If the artifact name exists
                    artifactExists = fileExists "ThemePark-${VERSION}.zip";
                    if(artifactExists) {
                        nexusArtifactUploader(
                            nexusVersion: NEXUS_VERSION,
                            protocol: NEXUS_PROTOCOL,
                            nexusUrl: NEXUS_URL,
                            groupId: 'ma.oncf.digital',
                            version: VERSION,
                            repository: NEXUS_REPOSITORY,
                            credentialsId: NEXUS_CREDENTIAL_ID,
                            artifacts: [
                                // Artifact generated such as .jar, .ear and .war files.
                                [artifactId: 'ThemePark',
                                classifier: '',
                                file: "ThemePark-${VERSION}.zip",
                                type: 'zip']
                           
                            ]
                        );
                    } else {
                        error "*** File: ${artifactPath}, could not be found";
                    }
                }
            }
        }

        
   
    }
}
