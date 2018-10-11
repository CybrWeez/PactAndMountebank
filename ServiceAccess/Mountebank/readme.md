## Mountebank
- Visit [Mountebank](http://www.mbtest.org/) for info. 
- Define imposters, like [config.ejs](./simpleresponse.ejs). Easier to just reference other files, to keep some separation. The imposters define stubs, which define responses and potential predicates.
- Install via `npm install -g mountebank`
- To start it up, run `mb --configfile <filename>`. Easiest to run from the directory where the file is. Now it will behave as defined in stubs. You're up and running!

## Run via docker
- [Dockerfile](./mb.dockerfile) as an example.
- Build image via `docker build --tag="specflow-mb" -f "mb.dockerfile" .`. Again, from same directory is easiest. If success, you'll see it in `docker images`.
- To start container, `docker run -t -i -p 2525:2525 -p 4545:4545 -p 4546:4546 --name='specflow-mb' specflow-mb`. In this case, we expose 3 ports from the container, so we need to map them to local ports (which we just keep the same). 
- Now you can execute commands in the container, for instance the mb command to start it up with a filename. If you want to copy some ejs/json files to container, run `docker cp <src_file> <container_name>:/<source folder>`.