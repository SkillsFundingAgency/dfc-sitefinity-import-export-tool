# Sitefinity Import/Export Tool

This tool is designed to simplify the process of downloading / uploading and removing content within Sitefinity. 

It is a standalone tool, which reads a `json` file that defines a workflow based on the schema below [also see example scripts](https://github.com/SkillsFundingAgency/tree/master/dfc-sitefinity-import-export-tool/example-scripts). The idea behind the tool is that it can be run as either part of a release process in either a manual or automated way. Projects that require content to be pushed to sitefinity can call the tool with a workflow file and the actions will be ran. This allows projects to version their CMS content with the source code of the project. 

## Running a workflow

Make sure you have updated the `appsettings.json` with the details for you target sitefinity environment then run 

The executes a workflow defined in the json file given by the `-f` parameter. Within a workflow there are 3 fundamental types of actions `create`, `delete` and `extract`. If you run a workflow with an `extract` step then it is best to also supply the `-o` parameter which defines the directory to write the extract data to. If you do not supply this it will just assume the current working directory of the application.

#### AppSetting.json configuration values

* **SiteFinityApiUrlbase** - The URI location of the Sitefinity instance to extract content from
* **SiteFinityApiWebService** - The name of the webservice instance to call.
* **SiteFinityClientId** - The Client ID to use when calling the webservice (the `ClientId`, `ClientSecret` and  `Scope` should be setup as per  the [this documentation](https://www.progress.com/documentation/sitefinity-cms/request-access-token-for-calling-web-services)
* **SiteFinityClientSecret** - The client secret associated with the `Client Id`
* **SiteFinityUsername** - The Sitefinity username to log in as
* **SiteFinityPassword** - The password assocated with the user defined in _SiteFinityUsername_
* **SiteFinityRequiresAuthentication** - Defines whether to use a auth token handshake or to login to SiteFinity anonymously.
* **SiteFinityApiAuthenicationEndpoint** - The the URI (relative to _SiteFinityApiUrlbase_) that defines the authentication endpoint (typically `SiteFinity/Authenticate/openid/connect/token`)  

### Workflow JSON Schema 

Each step type within the workflow consists of 2 common (required) properties these are `action` and `contentType`

* `action` - Is the type of workflow step the preceding structure will define. It can been either `create`, `delete` or `extract`.
* `contentType` - Is the type that you wish the action to be relative to. The value for this type is typically the developer name for the type in SiteFinity.

#### Create 

In addition to the the required properties above the `create` schema also adds two extra properties `data` and `relates`.  

    {
      "action": "create",
      "contentType": "shortquestions",
      "data": {
        "IsNegative": false,
        "Order": 10,
        "QuestionText": "I set myself goals in life",
        "Description": ""
      },
      "relates": [
        {
          "values": [
            "driver"
          ],
          "relatedType": { "property": "Title", "type": "Trait", "contentType": "traits" }
        }
      ]
    }
    
* `data` - Here we define the payload that will be pushed to the SiteFinity webservice endpoint. The schema that is required for this data can be found in the generated webservice documentation available through the SiteFinity frontend

* `relates` - Here we define a relationship between the parent content type defined in the `create` section and the `traits` content type. The value of the `property` field is the key that is used to resolve the Id of the instance to match for the relation relation. For example, here the `driver` Trait will be matched using the `Title` property. To do this the tool will extract the appropriate content type from the CMS and cache the results. These results are then used to try and resolve the ID of the instance where title matches. If the property is matched more than once the first will be used.  


#### Extract 

The extract content can be used directly and requires no additional attributes 

    { "action": "extract", "contentType": "shortquestions", "directory": "D:\sitefinity_extract" }
    

#### Delete 

The delete content can be used directly and requires no additional attributes 

    { "action": "delete", "contentType": "shortquestions" }

### Running an import 

    dotnet run -- run-workflow -f path/to/json/workflowfile 
    
or if presented with a the binary executable 

    Dfc.Sitefinity.ImportExport.Tool.exe run-workflow -f path/to/json/workflowfil
    
