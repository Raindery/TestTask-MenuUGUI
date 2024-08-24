using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Random = UnityEngine.Random;

public class PseudoAPI : IPseudoAPI
{
    private const string _jsonColors = @"
    {
        'colors':[
            {
                'name': 'Red',
                'color': 'C92C2C'
            },
            {
                'name': 'Green',
                'color': '1C721E'
            },
            {
                'name': 'Blue',
                'color': '2D6BC7'
            }
        ]
    }
";

    private const string _jsonTexts = @"
    {
        'texts':[
            'Text1', 'Text2', 'Text3'
        ]
    }
";
    
    public async UniTask<Colors> GetColors()
    {
        await RandomDelay();
        return JsonConvert.DeserializeObject<Colors>(_jsonColors);
    }

    public async UniTask<Texts> GetTexts()
    {
        await RandomDelay();
        return JsonConvert.DeserializeObject<Texts>(_jsonTexts);
    }

    private async UniTask RandomDelay()
    {
        TimeoutController timeoutController = new TimeoutController();
        await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0.5f, 2.5f)), cancellationToken:timeoutController.Timeout(TimeSpan.FromSeconds(1.5f)));
        timeoutController.Reset();
    }
}

[Serializable]
public class Test
{
    public List<Color> colors;
}
