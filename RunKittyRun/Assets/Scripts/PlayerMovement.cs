using System;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private CharacterController controller; // CharacterController.
    [SerializeField] private float speed = 6f; // Hız için parametre.
    [SerializeField] private float jumpSpeed = 15f; // Zıplama için parametre.
    [SerializeField] private float turnSmoothTime = 0.1f; // Dönüşü smoothlaştırmak için parametre.

    private float turnSmoothVelocity; // Ellememize gerek yok referans için bulunuyor.
    private float gravity = -20f; // Default değer.
    private float tempDirectionY = 0; // Temp Y değeri.

    void Update()
    {
        HandleMovement(); // Tüm hareketi ve zıplamayı hallettiğimiz fonksiyon.
        HandleCrouch(); // Tüm eğilme hallettiğimiz fonksiyon.
    }

    private void HandleCrouch()
    {
        if (!isLocalPlayer) return; // Eğer biz değilsek eğilme.

        if(Input.GetKey(KeyCode.LeftControl)) // CTRL'e basılınca
        {
            transform.localScale = new Vector3(1f, 0.5f, 1f); // Karakteri 0.5 scale'ine düşürür.
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Karakteri 1 scale'ine yükseltir.
        }
    }
    private void HandleMovement()
    {
        if (!isLocalPlayer) return; // Eğer biz değilsek hareket ettirme.

        float horizontal = Input.GetAxisRaw("Horizontal"); // X input.
        float vertical = Input.GetAxisRaw("Vertical"); // Z input.

        Vector3 direction = new Vector3(horizontal, 0, vertical); // Hareket yönü.
        
        if (controller.isGrounded) // Eğer yerdeysek.
        {
            if (Input.GetButtonDown("Jump")) // Zıplama tuşuna basıldıysa.
            {
                tempDirectionY = jumpSpeed; // Y ekseninde zıplama hızı ekliyoruz.
            }
        }
        
        // Yerçekimi ekliyoruz.
        tempDirectionY += gravity * Time.deltaTime;

        direction.y = tempDirectionY;
        
        controller.Move(direction * speed * Time.deltaTime); // CharacterController'ımıza bilgileri aktarıp hareket ettiriyoruz.
    }
}
