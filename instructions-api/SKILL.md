---
name: instructions-api
description: Instrucciones que el agente debe serguir para analizar el proyecto legalo de .NET.
---

# Instrucciones

1. Analizar en profundida y documentar lo solicitado en el proyecto legado
2. Documentar todo lo que se analice
3. La documentación debe estar en formato Markdown
4. Si es posible dar ejemplos de cómo utilizar una funcionalidad cuando sea necesario, hacerlo
5. Si se encuentra código comentado, no incluirlo en la documentación, pero mencionarlo
6. Si se encuentra código que no se utiliza, no incluirlo en la documentación, pero mencionarlo
7. Si se encuentra código que no se entiende, mencionarlo
8. Generar un archivo markdown que tenga un nombre descriptivo y que incluya todo lo analizado
9. Generar el archivo markdown en un directorio llamado docs en la raíz del proyecto, si no existe crearlo
10. Si se encuentran conexiones a softland, mencionar donde
11. Si se encuentran conexiones a softland, describir qué hacen

## Cuando usar
- En todo momento que se solicite analizar el proyecto legalo de .NET
- Cuando se solicite analizar una capa en especial
- Cuando se solicite analizar un archivo en especial
- Cuando se solicite analizar una funcionalidad en especial
- Cuando se solicite analizar una clase en especial
- Cuando se solicite analizar un método en especial
- Cuando se solicite analizar una variable en especial
- Cuando se solicite analizar una constante en especial
- Cuando se solicite analizar una interfaz en especial
- Cuando se solicite analizar una implementación en especial
- Cuando se solicite analizar una herencia en especial
- Cuando se solicite analizar una inyección de dependencias en especial
- Cuando se solicite buscar alguna funcionalidad en especial
- Cuando se solicite buscar algún error en especial
- Cuando se solicite buscar algún concepto en especial
- Cuando se solicite buscar algún patrón en especial
- Cuando se solicite buscar alguna conexión en especial

## Archivos que se deben omitir en el análisis

### Omitir en capa Api
- [ ] Directorio packages, contiene librerías compiladas y no nos interesa para el análisis
- [ ] Direction App_Start, contiene archivos de configuración y no nos interesa para el análisis
- [ ] Direction Assets, contiene archivos estáticos, css y scss que no son relevantes
- [ ] Directorio bin, contiene archivos binarios y no nos interesa para el análisis
- [ ] Directorio fonts, contiene archivos de fuentes y no nos interesa para el análisis
- [ ] Directorio obj, contiene archivos de objetos y no nos interesa para el análisis
- [ ] Directorio Properties, contiene archivos de propiedades y no nos interesa para el análisis
- [ ] Directorio Resources, contiene archivos de recursos y no nos interesa para el análisis
- [ ] Directorio Src, contiene archivos de código fuente para web, no es relevante
- [ ] Directorio Views, contiene archivos de vistas y no nos interesa para el análisis
- [ ] Archivo favicon.ico, contiene el icono de la aplicación y no nos interesa para el análisis
- [ ] Archivo Global.asax, contiene archivos de configuración y no nos interesa para el análisis
- [ ] Archivo Global.asax.cs, contiene archivos de configuración y no nos interesa para el análisis
- [ ] Archivo packages.config, contiene archivos de configuración y no nos interesa para el análisis
- [ ] Archivo Teamwork.Api.csproj, contiene archivos de configuración y no es relevante
- [ ] Archivo Teamwork.Api.csproj.user, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.config, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.Debug.config, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.Release.config, contiene archivos de configuración y no es relevante

### Omitir en capa Model
- [ ] Direction App_Start, contiene archivos de configuración y no nos interesa para el análisis
- [ ] Directorio bin, contiene archivos binarios y no nos interesa para el análisis
- [ ] Directorio obj, contiene archivos de objetos y no nos interesa para el análisis
- [ ] Directorio Properties, contiene archivos de propiedades y no nos interesa para el análisis
- [ ] Archivo Global.asax, contiene archivos de configuración y no nos interesa para el análisis
- [ ] Archivo packages.config, contiene archivos de configuración y no nos interesa para el análisis
- [ ] Archivo Teamwork.Model.csproj, contiene archivos de configuración y no es relevante
- [ ] Archivo Teamwork.Model.csproj.user, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.config, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.Debug.config, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.Release.config, contiene archivos de configuración y no es relevante

### Omitir en capa Repository
- [ ] Directorio bin, contiene archivos binarios y no nos interesa para el análisis
- [ ] Directorio obj, contiene archivos de objetos y no nos interesa para el análisis
- [ ] Directorio Properties, contiene archivos de propiedades y no nos interesa para el análisis
- [ ] Archivo Teamwork.Repository.csproj, contiene archivos de configuración y no es relevante
- [ ] Archivo Teamwork.Repository.csproj.user, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.config, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.Debug.config, contiene archivos de configuración y no es relevante
- [ ] Archivo Web.Release.config, contiene archivos de configuración y no es relevante

### Omitir de manera general
- [ ] Archivo .gitignore, contiene configuración de git, no es relevante
- [ ] Archivo README.md, contiene documentación general del proyecto, no es relevante
- [ ] Omitir código comentado