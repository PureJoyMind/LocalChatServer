# Chat App With SignalR
This is a personal project with the purpose of creating a chat application. 

## Basic Outline of Current Progress
I am making a server for chat. My design is this:
the database is a nosql type, each time a chat is opened a session entity is created. 
each message is also an entity in the database with fields like sender, session, text, dateTime, and such. for encryption, I want the clients to encrypt the messages and hold the key. so each time a new session is created the clients create a unique secret key for themselves, and when sending messages all of them are encrypted using the clients secret key. 
There are a few challenges with this approach. 
First, how can I make all the clients for a session have the same secret key for that session without exposing it to the server? Using asymmetric cryptography or Public/Private Key
need a GetSessions & GetMessages api for each active user. 

### Evaluating the Approach

Using end-to-end encryption is generally a good practice for securing communication, especially in sensitive applications like chat. However, there are some considerations:
* Key Management: Ensure that the process of key generation, exchange, and storage on the client side is secure. Keys should be adequately protected, and mechanisms for key rotation and revocation should be considered.

* Algorithm Selection: Choose strong encryption algorithms for securing the messages. Popular choices include ***AES*** for symmetric encryption and RSA or Elliptic Curve Cryptography for key exchange.

* User Experience: Consider the user experience when dealing with encryption. Users should be able to seamlessly exchange keys without a complex setup process.

* Key Lifecycle: Define a strategy for handling key lifecycle, including key expiration, renewal, and revocation, to maintain the security of the system over time.

## Application Workflow
on first register the user data is stored in an in-memory mongoDb instance. each user also has their public key in their document. 
if a message in db, has a deprecated user, keep the user name.
