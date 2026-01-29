# API Definition

## GET Endpoint for city coordinates
GET /api/location?city={cityName}

### Response structure:
{
	"country": ,
	"region": ,
	"city": ,
	"latitude": ,
	"longitude": 
}

### Query parameters
- cityName

### Staus codes: 
- 200
- 404

## POST Endpoint for location
POST /api/location

### Request structure:
{
	"country": ,
	"region": ,
	"city": ,
	"latitude": ,
	"longitude": 
}

### Staus codes: 
- 201
- 409 (For handling duplicates)
