rootProject.name = "politecon"
include("modules:common")
include("modules:importer")
include("modules:storage")
include("modules:api")
include("modules:services")
findProject(":modules:services")?.name = "services"
