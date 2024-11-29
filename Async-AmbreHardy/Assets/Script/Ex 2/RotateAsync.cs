using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class RotateAsync : MonoBehaviour
{
    [SerializeField] private GameObject _object;

    private CancellationTokenSource _cancellationTokenSource;

    public async void StartRotation()
    {
        StopRotation();

        _cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = _cancellationTokenSource.Token;

        try
        {
            await Rotation(token);
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Rotation annulée.");
        }
        finally
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }

    public void StopRotation()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
        }
    }

    private async Task Rotation(CancellationToken token)
    {
        float elapsedTime = 0f;
        float duration = 5f;

        while (elapsedTime < duration)
        {
            token.ThrowIfCancellationRequested();

            _object.transform.Rotate(0, 0, 1);

            elapsedTime += Time.deltaTime;

            await Task.Yield();
        }
    }
}
