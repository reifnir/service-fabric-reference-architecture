#to install sfctl from python3: pip install sfctl

IMAGE_NAME="my-sf-cluster"
IMAGE_EXISTS="$(docker images --format "{{.Repository}}" | grep "$IMAGE_NAME")"

if [ -z "$IMAGE_EXISTS" ]
then
    echo "Service Fabric OneBox image not found"
    echo "Building now..."
    docker build -t "$IMAGE_NAME" .
else
    echo "Service Fabric OneBox image exists: $IMAGE_EXISTS"
fi

echo "Starting cluster..."
docker run \
    --name sftestcluster \
    --detach \
    --volume /var/run/docker.sock:/var/run/docker.sock \
    --publish 19080:19080 \
    --publish 19000:19000 \
    --publish 25100-25200:25100-25200 \
    "$IMAGE_NAME"

echo "Connect sfctl to local cluster..."
sfctl cluster select --endpoint http://localhost:19080
