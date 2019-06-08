IMAGE_NAME="my-sf-cluster"

CONTAINER_ID="$(docker ps --filter "ancestor=$IMAGE_NAME" -q)"



if [ -z "$CONTAINER_ID" ]
then
    echo "Service Fabric OneBox container not running"
else
    echo "Service Fabric OneBox container found: $CONTAINER_ID"
    echo "Killing container..."
    docker rm -f sftestcluster
fi
