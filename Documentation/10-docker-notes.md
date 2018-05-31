# Docker Notes

## Local development
- Works pretty integrated with visual studio
- Currently no docker-compose support in Jetbrains Rider
    - Comment out the service that you are running with Rider
    - Create host file entries for docker urls to make it work with both dev & deploy environment

## Open interactive shell in Container

```bash
docker exec -it <CONTAINER_ID> bash
```