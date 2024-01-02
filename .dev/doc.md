## Backend

### Installation

```bash
dotnet restore # look at the PackageReference elements in your .csproj file and downloads the necessary packages.
```

Build the project:

```bash
dotnet build
```

Run the project:

```bash
dotnet watch run
```

Navitage to swagger at [http://localhost:5290/swagger/index.html](http://localhost:5290/swagger/index.html).

#### Database

```bash
docker build -t your-custom-postgres .
docker run --name your-postgres -d -p 5432:5432 your-custom-postgres
```

## Roadmap
* Job search engine
* Title
* Location
* Description
* Roadmap
* Search scrapped jobs
* Create job alerts
* Search based on resume
* Market trends analytics
* Export data
* Scrape job description, and create relative motivation letter
* Fill resume templates like EUROPASS with fillable fields

## TODO
* https://github.com/domaindrivendev/Swashbuckle.AspNetCore#include-descriptions-from-xml-comments

