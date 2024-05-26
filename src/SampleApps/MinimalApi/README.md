Sample.Minimal application is used to implement and test LSCore ecosystem.
To setup and run application clone it, and inside `appsettings.json` file (both Sample.Minimal.Api & Sample.Minimal.DbMigrations) update variables:
POSTGRES_USER, POSTGRES_PASSWORD, POSTGRES_DB, POSTGRES_HOST, POSTGRES_PORT

After you have updated appsettings, run entity framework database update on `Sample.Minimal.DbMigrations` project (it is both startup and migrations project) and you should have ready DB.

After that, simply run `Sample.Minimal.Api` application
