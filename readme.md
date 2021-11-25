## FormToPdf
This project demonstrates that the information in the form is added to the pdf template as a result of the API call triggered by the onSumbit event by Google Form, and then an e-mail including this filled template is sent to the recipients.
script.gs file makes the connection between Google Forms and Api and should be embedded in the created form. It can be changed to other services like MS Forms etc. since the api is standalone asset.

### Adding script to Google Form
* Create a form using https://docs.google.com/forms/
* In the form editor you will see the three dots top right corner. When you clicked that it will open a popup menu. Select the script editor.
* When you redirected to script editor, paste the script.gs content. and save it. It will automatically bind with the form.
* You can change the content of script file according to your needs. You can check the official Google Documentation.