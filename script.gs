function setUpTrigger() {
  const form = FormApp.getActiveForm()
  ScriptApp.newTrigger('sendPostRequest')
    .forForm(form)
    .onFormSubmit()
    .create();
}

function sendPostRequest(e) {
  var formResponse = e.response;
  var itemResponses = formResponse.getItemResponses();
  
  let data = {}

  itemResponses.forEach(r => {
    const indexer = r.getItem().getTitle()
    data[indexer] = r.getResponse()
  });

  var options = {
    'method': 'post',
    'contentType': 'application/json'
    'payload': JSON.stringify(data)
  }

  //console.log(result)

  UrlFetchApp.fetch('{URL of the consuming API}', options);
}
