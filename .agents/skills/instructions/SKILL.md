---
name: isntructions
description: Instrucciones básicas que debe serguir el agente de antigravity cuando analice los archivos de este proyecto
---

# Instrucciones

## Cuando usar
- cuando se pida analizar un archivo sql
- cuando se este analizando un procedimiento o funcion almacenada

## Checklist
- [ ] Analizar el archivo SQL
- [ ] Describir el objetivo del procedimiento o funcion almacenada
- [ ] Analizar los parámetros de entrada
- [ ] Analizar las variables internas
- [ ] Analizar las llamadas internas (funciones y vistas)
- [ ] Analizar la lógica de cálculo
- [ ] Analizar el retorno
- [ ] Documentar el resultado en un archivo md con el mismo nombre que el archivo sql
- [ ] Tablas afectadas, si es que hay y con que operaciones
- [ ] Omite el código comentado, pero menciona que existe
- [ ] Lo archivos md de salida deben ser generados en la raiz del proyecto, es decir, afuera del directorio analyzed

## Omitir
- Todo lo que hay en el directorio .others