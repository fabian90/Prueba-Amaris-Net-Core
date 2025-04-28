# Documentación de Despliegue

Este proyecto utiliza AWS CloudFormation para desplegar la infraestructura necesaria para una aplicación basada en EC2 y servicios relacionados. A continuación se describen los archivos de plantilla, los parámetros requeridos y los pasos para realizar el despliegue.

## Archivos de Plantilla

- **template.yaml**: Plantilla simple para lanzar una instancia EC2 Amazon Linux 2 para pruebas o desarrollo. Incluye la creación de un Security Group y una instancia EC2 básica.
- **cloudformation/vpc.yaml**: Define la VPC principal, subred pública e Internet Gateway necesarios para la red de la aplicación.
- **cloudformation/iam-roles.yaml**: Define los roles y políticas IAM requeridos para la instancia EC2, permitiendo acceso a servicios como DynamoDB, ECR y CloudWatch Logs.
- **cloudformation/ecs-cluster.yaml**: Plantilla principal para lanzar la instancia EC2 de la aplicación, asociada a la VPC, subred y roles definidos. Incluye configuración de Docker y despliegue automático del contenedor desde ECR.

## Parámetros Requeridos

### template.yaml
- **KeyName**: Nombre del par de llaves EC2 existente para acceso SSH.
- **InstanceType**: Tipo de instancia EC2 (por defecto t2.micro).
- **AmiId**: ID de la AMI de Amazon Linux 2 (por defecto ami-0c02fb55956c7d316).

### cloudformation/vpc.yaml
- No requiere parámetros externos.

### cloudformation/iam-roles.yaml
- No requiere parámetros externos.

### cloudformation/ecs-cluster.yaml
- **VpcId**: ID de la VPC donde se lanzará la instancia EC2 (obtenido tras desplegar vpc.yaml).
- **SubnetId**: ID de la subred pública (obtenido tras desplegar vpc.yaml).
- **KeyName**: Nombre del par de llaves EC2 para acceso SSH.

## Pasos para el Despliegue

1. **Desplegar la VPC**
   ```
   aws cloudformation deploy --template-file cloudformation/vpc.yaml --stack-name amaris-vpc
   ```
   Anota los valores de salida `VPCId` y `SubnetId`.

2. **Desplegar los Roles IAM**
   ```
   aws cloudformation deploy --template-file cloudformation/iam-roles.yaml --stack-name amaris-iam-roles
   ```

3. **Desplegar la Instancia EC2 y Recursos Asociados**
   ```
   aws cloudformation deploy --template-file cloudformation/ecs-cluster.yaml --stack-name amaris-ecs-cluster \
     --parameter-overrides VpcId=<VPC_ID> SubnetId=<SUBNET_ID> KeyName=<KEY_PAIR_NAME>
   ```
   Reemplaza `<VPC_ID>`, `<SUBNET_ID>` y `<KEY_PAIR_NAME>` por los valores correspondientes.

4. **(Opcional) Despliegue de plantilla simple**
   Si solo necesitas una instancia EC2 básica para pruebas, puedes usar `template.yaml`:
   ```
   aws cloudformation deploy --template-file template.yaml --stack-name amaris-simple \
     --parameter-overrides KeyName=<KEY_PAIR_NAME>
   ```

## Consideraciones de Seguridad
- El Security Group permite acceso SSH (puerto 22) desde cualquier IP (`0.0.0.0/0`). Se recomienda restringir este acceso a rangos de IPs confiables en ambientes productivos.
- Los roles IAM otorgan permisos amplios a servicios como DynamoDB y ECR. Ajusta las políticas según el principio de menor privilegio.

## Acceso a la IP Pública y Solución de Problemas de Conectividad

### Obtener la IP Pública de la Instancia EC2
- Una vez desplegada la instancia, puedes obtener la IP pública desde la consola de AWS EC2, en la sección "Instancias".
- También puedes usar el siguiente comando AWS CLI:
  ```
  aws ec2 describe-instances --filters "Name=tag:Name,Values=<NOMBRE_INSTANCIA>" --query "Reservations[*].Instances[*].PublicIpAddress" --output text
  ```
  Reemplaza `<NOMBRE_INSTANCIA>` por el nombre asignado a tu instancia.

### Configuración de Grupos de Seguridad y Puertos
- Asegúrate de que el Security Group asociado a la instancia EC2 permita el tráfico entrante en los puertos necesarios:
  - **22**: SSH (solo desde IPs confiables)
  - **80**: HTTP (si la aplicación expone un servicio web)
  - **443**: HTTPS (si aplica)
  - Otros puertos según los servicios que exponga tu aplicación
- Para modificar reglas, ve a la consola de EC2 > Grupos de seguridad > Editar reglas de entrada.

### Solución de Problemas Comunes de Conectividad
- **No puedes acceder por SSH:**
  - Verifica que la clave privada (.pem) sea la correcta y tenga permisos adecuados.
  - Asegúrate de que tu IP esté permitida en el Security Group.
  - Comprueba que la instancia esté en estado "running" y tenga una IP pública asignada.
- **No puedes acceder a la aplicación web:**
  - Verifica que el puerto correspondiente esté abierto en el Security Group.
  - Asegúrate de que la aplicación esté corriendo dentro de la instancia.
  - Usa `curl` o un navegador para probar la IP pública y el puerto expuesto.

### Recomendaciones
- Limita el acceso SSH solo a rangos de IPs confiables.
- Elimina reglas de acceso innecesarias una vez finalizada la configuración.
- Considera el uso de Elastic IP si necesitas una IP pública estática.

## Dependencias
- El despliegue de `ecs-cluster.yaml` depende de los recursos creados en `vpc.yaml` y `iam-roles.yaml`.
- Asegúrate de tener un par de llaves EC2 creado previamente en la región correspondiente.

## Salidas de los Stacks
Cada plantilla proporciona salidas útiles como IDs de recursos y direcciones IP públicas, que pueden consultarse con:
```
aws cloudformation describe-stacks --stack-name <STACK_NAME>
```

---

Para más detalles, revisa los archivos YAML en la carpeta `cloudformation` y ajusta los parámetros según tus necesidades.