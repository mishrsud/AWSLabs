#!/usr/bin/env bash

project="sqs-message-processor"
s3bucket="suds-lambda-bucket"
version="1.1"
suffix="1_1"
ENVIRONMENT="Development"

echo "Deploying ${project}"
aws s3 cp "s3://${s3bucket}/${project}/${version}/packaged.yaml" packaged.yaml
aws cloudformation deploy \
  --stack-name="${project}" \
  --template-file=packaged.yaml \
  --capabilities="CAPABILITY_NAMED_IAM" \
  --parameter-overrides \
      Environment=${ENVIRONMENT} \
  --no-fail-on-empty-changeset \
  --tags Project=${project} Version=${version}