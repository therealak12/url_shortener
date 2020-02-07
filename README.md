# URL shortener

A simple implementation of URL shortener using .Net Core framework.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

Download and install the prerequisites according to you operating system from [here](https://docs.microsoft.com/en-us/dotnet/core/install).

### Running the prject


```bash
cd url_shortener/src
dotnet run
```
### Usage

To get shorten a long URL:

```bash
curl -X POST -H "Content-Type: application/json" -d '{"LongUrl": "https://www.example.com"}' http://localhost:5000/urls

```
To redirect to the real path of a shortened URL:

```bash
curl -i http://localhost:5000/$shortened_url
```

### Running the tests


```bash
cd url_shortener/tests
dotnet test
```


## Built With

* [.NET Core](https://dotnet.microsoft.com/) - The web framework used


## Authors

* [Ahmad Karimi](https://therealak12.github.io/mktbkhn-rsm/)
