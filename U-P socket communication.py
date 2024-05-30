import socket

def send_command(command):
    host = 'localhost'
    port = 12345

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.connect((host, port))
        s.sendall(command.encode('utf-8'))

# Enviar comando para cambiar a Scene1
send_command('scene1')

# Puedo agregar lógica para esperar y enviar otro comando más tarde
# Enviar comando para cambiar a Scene2
send_command('scene2')
print ("Hola")