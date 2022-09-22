function ResponseObject(code, data=null, errors=null) {
  this.code = code,
  this.data = data,
  this.errors = errors
}

const methods = {
  GET:'GET',
  POST:'POST',
  PUT:'PUT',
  DELETE:'DELETE'
}

async function callApi(endpoint,method=methods.GET,body=null){
  var headers = {
    'Accept': 'application/json',
    'Content-Type': 'application/json',
  }
  var requestBody = body!=null?JSON.stringify(body):""
  var url = `https://localhost:7263/${endpoint}`

  var initialValues = {
    method:method,
    headers:headers,
  }
  method==methods.GET? initialValues.body = undefined : initialValues.body = requestBody


  try{
    const response = await fetch(url,{...initialValues});
    let data = await response.json()
    if(response.ok){
      
      return new ResponseObject(response.status,data)
    }
    else{
      return new ResponseObject(response.status,null,data)
    }
  }catch(e){
    console.log(e)
    return new ResponseObject(0,null,"Algo anda mal")
  }
}

export {methods,callApi}