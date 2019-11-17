# Summary
A sample application that demonstrates a Lambda triggered by SQS using a SAM CloudFormation template.

# Details
The idea being demonstrated is as follows
- An SQS message triggers the lambda
- The lambda calls an HTTP API and receives a throttled response, i.e. a HTTP 429 too many requests response.
- The lambda requeues the message back to the queue it received from with a delay attribute set such that it can retry calling the HTTP API later