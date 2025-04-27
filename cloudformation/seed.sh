# Insertar datos en la tabla Fondos
aws dynamodb put-item \
    --table-name Fondos \
    --item '{
        "IdFondo": {"S": "1"},
        "Nombre": {"S": "FPV_EL CLIENTE_RECAUDADORA"},
        "MontoMinimo": {"N": "75000"},
        "Categoria": {"S": "FPV"},
        "Descripcion": {"S": "Fondo de pensiones voluntarias para recaudo"},
        "Activo": {"BOOL": true}
    }'

aws dynamodb put-item \
    --table-name Fondos \
    --item '{
        "IdFondo": {"S": "2"},
        "Nombre": {"S": "FPV_EL CLIENTE_ECOPETROL"},
        "MontoMinimo": {"N": "125000"},
        "Categoria": {"S": "FPV"},
        "Descripcion": {"S": "Fondo de pensiones asociado a Ecopetrol"},
        "Activo": {"BOOL": true}
    }'

# Insertar datos en la tabla Clientes
aws dynamodb put-item \
    --table-name Clientes \
    --item '{
        "IdCliente": {"S": "C001"},
        "Nombre": {"S": "Juan"},
        "Apellidos": {"S": "Perez"},
        "Ciudad": {"S": "Bogotá"},
        "Saldo": {"N": "500000"},
        "Documento": {"S": "12345678"},
        "Email": {"S": "juan.perez@email.com"},
        "Telefono": {"S": "3001234567"},
        "FechaRegistro": {"S": "2024-04-25"}
    }'

aws dynamodb put-item \
    --table-name Clientes \
    --item '{
        "IdCliente": {"S": "C002"},
        "Nombre": {"S": "Maria"},
        "Apellidos": {"S": "Lopez"},
        "Ciudad": {"S": "Medellín"},
        "Saldo": {"N": "200000"},
        "Documento": {"S": "87654321"},
        "Email": {"S": "maria.lopez@email.com"},
        "Telefono": {"S": "3019876543"},
        "FechaRegistro": {"S": "2024-04-25"}
    }'

# Insertar datos en la tabla Transacciones
aws dynamodb put-item \
    --table-name Transacciones \
    --item '{
        "IdTransaccion": {"S": "T001"},
        "IdCliente": {"S": "C001"},
        "IdFondo": {"S": "1"},
        "Tipo": {"S": "Apertura"},
        "Monto": {"N": "75000"},
        "Fecha": {"S": "2024-04-25"},
        "MedioNotificacion": {"S": "Email"},
        "Descripcion": {"S": "Apertura de fondo con FPV_RECAUDADORA"}
    }'

aws dynamodb put-item \
    --table-name Transacciones \
    --item '{
        "IdTransaccion": {"S": "T002"},
        "IdCliente": {"S": "C002"},
        "IdFondo": {"S": "2"},
        "Tipo": {"S": "Cancelación"},
        "Monto": {"N": "125000"},
        "Fecha": {"S": "2024-04-25"},
        "MedioNotificacion": {"S": "SMS"},
        "Descripcion": {"S": "Cancelación de fondo FPV_ECOPETROL"}
    }'
