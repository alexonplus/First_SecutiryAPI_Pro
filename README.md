# SecurityAPI - Exercise 1

An ASP.NET Core Web API project featuring JWT authentication and Swagger documentation.

## Features
* **6 Endpoints**: Fully implemented Auth, Courses, and WeatherForecast controllers.
* **JWT Auth**: Secured endpoints using Bearer token scheme.
* **Auto-Prefix**: Swagger is configured to handle the 'Bearer' prefix automatically.

## How to use
1. Run the project (F5).
2. Authenticate via `POST /api/Auth/login` with `admin`/`password`.
3. Click **Authorize**, paste the token, and click Authorize.
4. Access `GET /WeatherForecast` to verify authorization (200 OK).



## 3 Common Mistakes I Solved Today

1. **Server Connectivity Issues**: I encountered a "Failed to fetch" error in Swagger because the local server had crashed with exit code -1. I resolved this by ensuring the backend was actively running before sending requests.
2. **Authorization Header Errors**: I fixed a 401 Unauthorized status caused by a duplicate "Bearer" prefix in the authorization field. I updated `Program.cs` to handle the scheme automatically.
3. **Configuration Syntax**: I corrected startup failures caused by improper JSON formatting in the appsettings, which was preventing the JWT middleware from initializing correctly.