# SIGA


## Guidelines // Change this heading later

There should not be logical code in the Form classes. They should only receive data and display it. The logic should be in the BusinessRules class.

The BusinessRules class should give data to the Presenter. 
The Presenter will then pass the data to the Form, besides choosing the right Form to display.

Why this bureocracy? Because it makes the code less coupled. We can switch between different interfaces without having to change the BusinessRules class.

## The Model-View-Presenter pattern
This project will use the MVP pattern, which is a variation of the MVC pattern. It is more suited for desktop applications than the MVC pattern.

The Presenter will be responsible for choosing the right Form to display.
For example, after the user has logged in, the Presenter will choose the right Form to display depending on the user's role.

The View is just the Form.
The Model is the BusinessRules class. It will contain the logic of the application.