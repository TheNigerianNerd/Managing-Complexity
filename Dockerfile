# Log in to Docker Hub first
docker login

# Set engineering-builder as your default Build Cloud builder
docker buildx use cloud-engineering-builder --global

# Build and push a multi-arch image
docker buildx build \
  --builder cloud-engineering-builder \
  --platform linux/amd64,linux/arm64 \
  -f Pragmatic-Programmer/Dockerfile \
  -t your-dockerhub-user/todo-api:latest \
  --push .
