# das-subscriptions

## Developer setup

### Requirements

In order to run this solution locally you will need the following:

* [.NET Core SDK >= 2.1.5](https://www.microsoft.com/net/download/)
* (VS Code Only) [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)
* [Docker for X](https://docs.docker.com/install/#supported-platforms)

### Environment Setup

The default development environment uses docker containers to host it's dependencies.

* Redis
* Elasticsearch
* Logstash

On first setup run the following command from _**/setup/containers/**_ to create the docker container images:

`docker-compose build`

To start the containers run:

`docker-compose up -d`

You can view the state of the running containers using:

`docker ps -a`


### Application logs
Application logs are logged to [Elasticsearch](https://www.elastic.co/products/elasticsearch) and can be viewed using [Kibana](https://www.elastic.co/products/kibana) at http://localhost:5601

## License

Licensed under the [MIT license](LICENSE)
