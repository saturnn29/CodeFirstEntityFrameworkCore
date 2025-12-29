pipeline {
    agent any
    environment {
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
                withCredentials([usernamePassword(credentialsId: 'docker-hub-creds', passwordVariable: 'DOCKER_PASS', usernameVariable: 'DOCKER_USER_ENV')]) {
                    sh "echo \$DOCKER_PASS | docker login -u \$DOCKER_USER_ENV --password-stdin"
                    sh "docker push ${DOCKER_USER}/${IMAGE_NAME}:${BUILD_NUMBER}"
                    sh "docker push ${DOCKER_USER}/${IMAGE_NAME}:latest"
                }
            }
        }
        stage('Test Run') {
            steps {
                withCredentials([string(credentialsId: 'oracle-connection-string', variable: 'SECURE_ORA_CONN')]) {
                    sh "docker run --rm -e POSTGRES_MOVIES_LOCAL_CONNSTR='${SECURE_ORA_CONN}' ${DOCKER_USER}/${IMAGE_NAME}:latest || true"
                }
            }
        }
    }
}
