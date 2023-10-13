pipeline{
    agent any
    triggers{
        pollSCM("* * * * *")
    }
    stages{
        stage("Build docker"){
            steps{
                sh "echo $PATH"
            }
        }
    }
}
