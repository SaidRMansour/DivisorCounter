pipeline{
    agent any
    triggers{
        pollSCM("* * * * *")
    }
    stages{
        stage('Build') {
            steps {
                sh "/usr/local/bin/docker compose build"
            }
        }
        stage('Deliver') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSOWRD')]){
                    sh 'docker login -u $USERNAME -p $PASSWORD'
                    sh "docker compose push"
                }
            }
        }
        stage('Deploy') {
            steps {
                sh "/usr/local/bin/docker compose up --build counter-service"
            }
        }
    }
}
