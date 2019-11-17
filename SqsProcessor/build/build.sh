#!/usr/bin/env bash

project="sqs-message-processor"
version=1.1

echo "Compile and Run tests"
docker-compose -f ../docker-compose.yml up --build --exit-code-from sqs-message-processor-tests sqs-message-processor-tests
echo "Copying binaries from docker container"
mkdir ./output || true
docker cp sqs-message-processor-tests:./app/MessageProcessor/out ./output/publish
docker-compose -f ../docker-compose.yml down

aws cloudformation package \
    --template-file="./cloudformation.yaml" \
    --output-template-file="./packaged.yaml" \
    --s3-prefix="${project}/${version}" \
    --s3-bucket="suds-lambda-bucket"

aws s3 cp "./packaged.yaml" "s3://suds-lambda-bucket/${project}/${version}/"