
pipeline{
    
   agent any

    
    
    environment{
		NEXUS_VERSION = "nexus2"
        // This can be http or https
        NEXUS_PROTOCOL = "http"
        // Where your Nexus is running
        NEXUS_URL = "artifacts.hikanosland.com/nexus"
        // Repository where we will upload the artifact
        NEXUS_REPOSITORY = "repository-example"
        // Jenkins credential id to authenticate to Nexus OSS
        NEXUS_CREDENTIAL_ID = "nexus-credentails"
		
        // set the build Version
	    VERSION = "2.${java.time.LocalDate.now().getYear()}.${java.time.LocalDate.now().getDayOfYear().toString()}.${java.lang.String.format("%02d", java.time.LocalTime.now().getHour())}${java.lang.String.format("%02d", java.time.LocalTime.now().getMinute())}"
    

  
    }
    
    stages{
	     stage ('CLEAN WORKSPACE') {
          steps {
              cleanWs()
           }
        }
        
        stage("CHECKOUT CODE") {
            steps {
                script {
                    // Let's clone the source                   
					// https://github.com/kametepe/ThemeParkCore.git
				    //git url: 'https://gitlab.com/kametepe/theme-park.git', branch: 'master', credentialsId: 'gitlab-credentails'
                }
            }
        }

        
       Stage('RESTORE'){
            steps{
                echo "*********************START RESTORE*********************"
                sh "dotnet restore"
            }
        }
        
	    stage('Dependency Check') {
            steps {
                dependencyCheck additionalArguments: '', odcInstallation: 'OWASP-Dependency-Checker'
                dependencyCheckPublisher pattern: '**/dependency-check-report.xml'
            }
        }	
        
        stage('Build and Set Dll Version ') {
            steps {
                echo 'Perform Build'
				bat "dotnet build --configuration Release /p:Version=1.2.3.4"
            }
        }
        
         stage("SAST :  SonarQube analysis") {
            steps {
                echo 'Unit test , Code Coverage and Perform Test Coverage with SonarQube'
            }
        }
        stage('SAST : Quality Gate') {
            steps {
                echo 'Check Quality Gate'
            }
        }
        stage ("DAST : Deploy Package") {
            steps {
                echo 'Push Artifacts to Remote Server'
            }
        }
        
         stage('DAST') {
            steps {
                echo 'Perform DAST scanns'
            }
        }
        
		stage('Deploy Artifacts') {
            steps {
                echo 'Push Artifacts to Repository'
            }
        }
        
        stage('Last'){
            steps{
                echo "********************* ALL END*********************"
            }
        }
        
        
    }
       
    
   post {
        always {
            echo 'One way or another, I have finished'
            /*deleteDir()*/ /* clean up our workspace */
        }
        success {
            echo 'I succeeeded!'
        }
        unstable {
            echo 'I am unstable :/'
        }
        failure {
            echo 'I failed :('
        }
        changed {
            echo 'Things were different before...'
        }
    }
    
}