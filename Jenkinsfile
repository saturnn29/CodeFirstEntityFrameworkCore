pipeline {
    agent any
    environment {
        ORA_CONN = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.197.129)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=pdb1)));User Id=system;Password=miserable;"
        DOCKER_USER = "duylinh29"
        IMAGE_NAME = "movie-app"
    }
    stages {
        stage('Checkout') {
            steps { checkout scm }
        }
        stage('Build Image') {
            steps {
                sh "docker build -t ${DOCKER_USER}/${IMAGE_NAME}:${BUILD_NUMBER} ."
                sh "docker tag ${DOCKER_USER}/${IMAGE_NAME}:${BUILD_NUMBER} ${DOCKER_USER}/${IMAGE_NAME}:latest"
            }
        }
        stage('Push to Registry') {
            steps {
                // This logs you into Docker Hub using the Jenkins Credentials ID
                withCredentials([usernamePassword(credentialsId: 'docker-hub-creds', passwordVariable: 'DOCKER_PASS', usernameVariable: 'DOCKER_USER_ENV')]) {
                    sh "echo \$DOCKER_PASS | docker login -u \$DOCKER_USER_ENV --password-stdin"
                    sh "docker push ${DOCKER_USER}/${IMAGE_NAME}:${BUILD_NUMBER}"
                    sh "docker push ${DOCKER_USER}/${IMAGE_NAME}:latest"
                }
            }
        }
        stage('Test Run') {
            steps {
                // Run the container from the newly pushed image
                sh "docker run --rm -e POSTGRES_MOVIES_LOCAL_CONNSTR='${ORA_CONN}' ${DOCKER_USER}/${IMAGE_NAME}:latest"
            }
        }
    }
}
