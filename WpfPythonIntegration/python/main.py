# -*- coding: utf-8 -*-

import sys

def main():
    print("Esperando mensajes de C#...")
    sys.stdout.flush()  # Asegúrate de vaciar la salida estándar para enviar el mensaje inmediatamente

    while True:
        # Leer el mensaje desde la entrada estándar
        message = input()

        if message.lower() == "exit":
            print("Cerrando script por petición.")
            sys.stdout.flush()
            break

        # Procesar el mensaje recibido
        print(f"Mensaje recibido desde C#: {message}")
        sys.stdout.flush()  # Asegúrate de vaciar la salida estándar para que se vea en la aplicación

if __name__ == "__main__":
    main()
