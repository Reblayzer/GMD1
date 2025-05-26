using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueController : MonoBehaviour
{
  [SerializeField] private Transform directionalLight;
  [SerializeField] private TextMeshProUGUI orboText;
  [SerializeField] private TextMeshProUGUI coreText;
  [SerializeField] private float fadeDuration = 1f;

  private void Awake()
  {
    if (orboText != null) orboText.gameObject.SetActive(false);
    if (coreText != null) coreText.gameObject.SetActive(false);
  }

  public void StartDialogueSequence(CollisionManager collisionManager, float shardTransferDelay = 0.05f)
  {
    StartCoroutine(DialogueSequence(collisionManager, shardTransferDelay));
  }

  private IEnumerator DialogueSequence(CollisionManager collisionManager, float delayBetweenShards)
  {
    // Fade in Orbo's text
    yield return FadeText(orboText, fadeIn: true);
    yield return new WaitForSeconds(2f); // visible duration
    yield return FadeText(orboText, fadeIn: false);

    // Start shard transfer
    yield return StartCoroutine(collisionManager.TransferShardsToCore(delayBetweenShards));

    yield return new WaitForSeconds(1f); // after last shard

    // Fade in Core's text
    yield return FadeText(coreText, fadeIn: true);
    yield return new WaitForSeconds(3f); // visible duration
    yield return FadeText(coreText, fadeIn: false);

    // Wait 1 second after Core's message
    yield return new WaitForSeconds(0.5f);

    // Slowly rotate light over 3 seconds
    yield return RotateLightX(5f);
  }

  private IEnumerator FadeText(TextMeshProUGUI text, bool fadeIn)
  {
    if (text == null)
      yield break;

    text.gameObject.SetActive(true);

    float t = 0f;
    Color original = text.color;
    float startAlpha = fadeIn ? 0f : 1f;
    float endAlpha = fadeIn ? 1f : 0f;

    while (t < fadeDuration)
    {
      t += Time.deltaTime;
      float blend = Mathf.Clamp01(t / fadeDuration);
      float alpha = Mathf.Lerp(startAlpha, endAlpha, blend);
      text.color = new Color(original.r, original.g, original.b, alpha);
      yield return null;
    }

    text.color = new Color(original.r, original.g, original.b, endAlpha);

    if (!fadeIn)
      text.gameObject.SetActive(false);
  }

  private IEnumerator RotateLightX(float duration)
  {
    if (directionalLight == null)
      yield break;

    Quaternion startRotation = directionalLight.rotation;
    Quaternion endRotation = Quaternion.Euler(180f, directionalLight.eulerAngles.y, directionalLight.eulerAngles.z);

    float t = 0f;
    while (t < duration)
    {
      t += Time.deltaTime;
      float blend = Mathf.Clamp01(t / duration);
      directionalLight.rotation = Quaternion.Slerp(startRotation, endRotation, blend);
      yield return null;
    }

    directionalLight.rotation = endRotation;

    yield return new WaitForSeconds(1f);

    // After rotation is done
    if (LevelUIManager.Instance != null)
      LevelUIManager.Instance.ShowGameCompleted();
  }

}
