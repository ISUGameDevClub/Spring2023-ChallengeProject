using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSlider : MonoBehaviour
{
    public Slider speedSlider;
    public Text speedText;
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float currentSpeed = 2f;

    private void Start()
    {
        // Initialize the slider and text values
        speedSlider.minValue = minSpeed;
        speedSlider.maxValue = maxSpeed;
        speedSlider.value = currentSpeed;
        UpdateSpeedText(currentSpeed);
    }

    private void Update()
    {
        // Update the game speed based on the slider value
        currentSpeed = speedSlider.value;
        Time.timeScale = currentSpeed;

        // Update the speed text to show the current speed
        UpdateSpeedText(currentSpeed);
    }

    private void UpdateSpeedText(float speed)
    {
        // Update the speed text to show the current speed
        speedText.text = "Speed: " + speed.ToString("F1") + "x";
    }
}
