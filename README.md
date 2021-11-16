SayoloTest

Sayollo Unity interview home challenge
● Please feel free to ask me any question you have
● Purpose
○ The purpose of this challenge is to examine relevant basic skills to be a
successful Unity developer in Sayollo.
● Mandatory recruitments
○ The code works well without any bugs / other crashes
○ Well structure logic and clean code
○ Good explanations about how and why you choose to implement it as you did
○ Unity version 2019.3.X
● Less important
○ UI - I am less interested in the graphical aspects of this exercise
○ Complicated design patterns - keep things simple
● The needed implementation - create an SDK that performs the following:
○ Get and present a video ad
■ Send a GET request to:
➢ https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/ad
/vast
■ As a response, you will receive a VAST XML containing a video
■ Present the video to the screen
■ Save the video on the device
○ Get and present purchase item ad
■ Send a POST request to:
➢ https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/
gcom/ad
➢ The body should contain any valid JSON
■ As a response, you will receive a JSON containing few details on the
purchased item such as:
➢ Title
➢ Item_image
➢ Currency
➢ Currency_sign
■ Use the response details to present the screen some kind of simple
purchase screen containing all the details and ask the user to enter
➢ Email
➢ Credit card number
➢ Expiration date
■ Send the POST request containing all the information inserted by the user
to:
➢ https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/
gcom/action
➢ Body as a JSON
● How to submit
○ Upload the code to a Github repo and share it with me - user name - shaibouju
○ Write in the read me file some explanation about the code

this code uses own HttpClient class for networking to send http requests. Video and image downloading are also described in it. 
AdvertisementVideoDisplayer class delegates download logic to HttpClient and handles ad video displaying over the 3d plane.
ItemPurchaser class is responsible for gathering order item information through HttpClient class and displaying it's information. 
ItemPurchaser also has a method to send order transaction.