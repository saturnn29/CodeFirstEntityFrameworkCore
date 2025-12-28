pipeline {
    agent any
    environment {
        ORA_CONN = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.197.129)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=pdb1)));User Id=system;Password=miserable;"
    }
    stages {
        stage('Build') {
            steps {
                sh 'docker build -t movie-app:final .'
            }
        }
        stage('Test Connectivity') {
            steps {
                // Run the app and ensure it can at least talk to the DB
                sh "docker run --rm -e POSTGRES_MOVIES_LOCAL_CONNSTR='${ORA_CONN}' movie-app:final"
            }
        }
    }
}
