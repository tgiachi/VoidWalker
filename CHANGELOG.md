# Change Log

All notable changes to this project will be documented in this file. See [versionize](https://github.com/versionize/versionize) for commit guidelines.

<a name="0.0.1"></a>
## [0.0.1](https://www.github.com/tgiachi/VoidWalker/releases/tag/v0.0.1) (2024-09-25)

### Features

* add new HelloRequestPacket class to handle hello request packets in network ([abde5de](https://www.github.com/tgiachi/VoidWalker/commit/abde5de537f559874ca4a5872659b875e0ee1f3e))
* add player connection and disconnection events with session management ([a1bd10a](https://www.github.com/tgiachi/VoidWalker/commit/a1bd10af67a13153fa1aa17f3b5e5dca7675f609))
* add Redbus event handling to PlayerConnectedEvent, PlayerDisconnectedEvent, ([f88d1af](https://www.github.com/tgiachi/VoidWalker/commit/f88d1afd1ef18c39b1c6cd141ce999c51d095544))
* **.gitignore:** add pattern to ignore all files inside .idea directory ([b4d2b68](https://www.github.com/tgiachi/VoidWalker/commit/b4d2b68806612852aa84733199921cc664ec093f))
* **attributes:** add ScriptFunctionAttribute and ScriptModuleAttribute to define script functions and modules ([7da9661](https://www.github.com/tgiachi/VoidWalker/commit/7da96617d1c058e1fcf53cac0678d7ae7e800aa1))
* **auth:** add RegisterUserAsync method to LoginService for user registration ([5e37b40](https://www.github.com/tgiachi/VoidWalker/commit/5e37b40b7bfa1f00498e03c71502e4661a277c76))
* **auth-service:** add Dockerfile for auth-service to build and run the application in a container ([0c1e9dc](https://www.github.com/tgiachi/VoidWalker/commit/0c1e9dc6205460dd40821c2a9f50ce8a30ed143d))
* **AuthService:** add AuthServiceDataAccess class for handling data access logic in the AuthService module ([3125196](https://www.github.com/tgiachi/VoidWalker/commit/3125196bf16180453de18c47f22a6af1783a61d5))
* **AuthService:** add AuthServiceDbContext for managing user and role entities ([e2341f7](https://www.github.com/tgiachi/VoidWalker/commit/e2341f7800ba00d4a835620124e9f29037afd4e7))
* **AuthService:** add DbContext configuration for Npgsql with SnakeCaseNamingConvention ([e2c5fdb](https://www.github.com/tgiachi/VoidWalker/commit/e2c5fdb9c3df84b4e3e8c5d7a1d31d79d5291a84))
* **AuthService:** add LoginHub to handle SignalR connections for login functionality ([d7f1fef](https://www.github.com/tgiachi/VoidWalker/commit/d7f1fef9c1caa109566cb43c4226cfc342ec3546))
* **AuthService:** add new AuthService project with weather forecast endpoint and Swagger support ([748d0c2](https://www.github.com/tgiachi/VoidWalker/commit/748d0c2e2134092a0abc73f73a95153224cec0d9))
* **core:** add ScriptClassData record to store class type in ScriptClassData.cs ([64a952e](https://www.github.com/tgiachi/VoidWalker/commit/64a952e754889da27487a96561111faea1b3c70c))
* **Data:** add DataLoadedEvent record to handle data loading events ([55992e1](https://www.github.com/tgiachi/VoidWalker/commit/55992e1e750c5450db3256367e7c87e69a9460eb))
* **docker-compose:** add Redis server and port configuration for services to enable caching ([a8078f1](https://www.github.com/tgiachi/VoidWalker/commit/a8078f1e54403c818374ac75b39c752f3f78936c))
* **Dockerfile.game-service:** add Dockerfile for game-service to build, publish, and run the application in a containerized environment ([5d3a15f](https://www.github.com/tgiachi/VoidWalker/commit/5d3a15f2ba5be28d98d1fa624d739fa56bc6594a))
* **events:** add IsBroadcast property to SendOutputEvent for broadcast functionality ([c059bba](https://www.github.com/tgiachi/VoidWalker/commit/c059bba5b7f6d145f5c275a08d0b131881be1400))
* **events:** add SendListOutputEvent to handle multiple network packets in a single event ([5cafc11](https://www.github.com/tgiachi/VoidWalker/commit/5cafc117e42eea83f2abf3b5ca102595ffd61955))
* **Interfaces:** add IShardService interface to define methods for shard operations ([5497a6a](https://www.github.com/tgiachi/VoidWalker/commit/5497a6a7dbaa7b952b0c615cda159b091350157f))
* **Json Mapping:** implement JsonTypeAttribute and related classes for JSON mapping support ([34aa7bd](https://www.github.com/tgiachi/VoidWalker/commit/34aa7bd8c96e16f490efefb0f67f2c8e06345318))
* **LoginHub.cs:** add LoginAsync method to handle user login requests and send appropriate response packets ([47e9f40](https://www.github.com/tgiachi/VoidWalker/commit/47e9f40029250ad03aaa11c2b5bc07d49bab9237))
* **LoginService.cs:** add InitializeAsync method to LoginService for async initialization ([3444464](https://www.github.com/tgiachi/VoidWalker/commit/344446416233e1a2084864b7f2a985e3cb20f1ea))
* **network:** add LoginRequestPacket and LoginResponsePacket for handling login requests and responses ([96ec955](https://www.github.com/tgiachi/VoidWalker/commit/96ec955182e3f0cd8c81dfd759d99732cc48a88a))
* **Network:** add IncomingNetworkPacket class to handle incoming network packets ([c183f8a](https://www.github.com/tgiachi/VoidWalker/commit/c183f8a32db17e501a976fea23bf8747c0e9b4ee))
* **OutputMessageEventHandler:** add logging for broadcasting and session messages to improve traceability ([fe2230e](https://www.github.com/tgiachi/VoidWalker/commit/fe2230ea10bc2e086b27195bf6b551cb9237cf9a))
* **Program.cs:** integrate Serilog for enhanced logging capabilities in the application ([a48cc7b](https://www.github.com/tgiachi/VoidWalker/commit/a48cc7bba4e6367d81300d1cf4616c2701126624))
* **redis:** add Redis configuration and credentials management for caching ([27606c3](https://www.github.com/tgiachi/VoidWalker/commit/27606c34165c2e3454e7fed4e68c7eb9a04d6a10))
* **redis:** add SubscribeAsync and PublishAsync methods to IRedisCacheService interface and implement them in RedisCacheService class to enable subscribing to and publishing messages on Redis channels. This allows for real-time communication and event handling within the application. ([3987d2d](https://www.github.com/tgiachi/VoidWalker/commit/3987d2d5b0580c26fb459440d08b1682194d9dc3))
* **ScriptEngineExecutionResult.cs:** add ScriptEngineExecutionResult class with Result and Exception properties ([c2806c5](https://www.github.com/tgiachi/VoidWalker/commit/c2806c5f23d188c2d508f24dd354ac58a5468967))
* **SeedTypeData.cs:** convert SeedTypeData class to a record for improved readability ([4c134ba](https://www.github.com/tgiachi/VoidWalker/commit/4c134ba37616af5de02686b3bfad370cd3bfb5da))
* **Taskfile.yml:** add dependencies for build, tests, docker-compose:build, and docker-compose:up tasks ([df0db4e](https://www.github.com/tgiachi/VoidWalker/commit/df0db4e62be23fb7549c7ae141186d8148d8b659))
* **Taskfile.yml:** add new tasks 'tests', 'docker-compose:up', 'docker-compose:down' for running tests and managing docker-compose ([5ddb282](https://www.github.com/tgiachi/VoidWalker/commit/5ddb282aae3891d615ee385a8f02e8362c7927fe))
* **Taskfile.yml:** update docker-auth:build task to use specific Dockerfile for auth service image ([25fab47](https://www.github.com/tgiachi/VoidWalker/commit/25fab477de815c3fa3062d2c306dee6a35259516))
* **UserEntity:** create UserEntity class to represent user data in the database ([bd132fe](https://www.github.com/tgiachi/VoidWalker/commit/bd132fe736ef569125b70291f3fbbb1e1404b4a9))
* **VoidWalker:** add version tag with value 0.0.1 to all project files for consistency ([6a1468b](https://www.github.com/tgiachi/VoidWalker/commit/6a1468bb423abee5552309017999d1a4c02bd091))
* **VoidWalker.AuthService:** add UserRoleEntity to DbContext to establish ([2204b1e](https://www.github.com/tgiachi/VoidWalker/commit/2204b1ef530704f9027b7424f73e1483255b83e9))
* **VoidWalker.Engine.Core:** add ConfigSectionNotFoundException class to handle ([337c4fa](https://www.github.com/tgiachi/VoidWalker/commit/337c4faa14b8481dc9afe4be2ed51ae55255126f))
* **VoidWalker.Engine.sln:** add VoidWalker.Engine.Server project to solution ([b0a9ef6](https://www.github.com/tgiachi/VoidWalker/commit/b0a9ef6e5623ef358e62dd14f5c0fdcfc09abfb2))
* **workspace.xml:** add workspace.xml file for project configuration ([86c52e2](https://www.github.com/tgiachi/VoidWalker/commit/86c52e2a773e37649cc13143e860b505e51b55bb))

### Bug Fixes

* **Dockerfile:** fix MODE environment variable assignment by changing 'MODE prod' to 'ENV MODE=prod' for consistency ([a74821e](https://www.github.com/tgiachi/VoidWalker/commit/a74821ef9cc55ab9318fc1c843638a468bf6744d))
* **Program.cs:** remove unnecessary comment and update Authority URL ([ffc8a36](https://www.github.com/tgiachi/VoidWalker/commit/ffc8a361d13c92b702d0e8bce45522b2d22f73a9))

