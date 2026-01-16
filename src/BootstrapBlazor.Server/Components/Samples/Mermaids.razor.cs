// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// MermaidViews
/// </summary>
public partial class Mermaids
{
    private Mermaid _mermaid = default!;

    private MermaidType Options { get; set; } = new();

    private string CustomStyleString { get; } = """
        flowchart LR
        A[start] -->
        B{Whether the conditions are met?}
        B -- yes --> C[Perform tasks 1]
        B -- no --> D[Perform tasks 2]
        C --> E{Condition checks}
        D --> E
        E -- The conditions are established --> F[Sub-processes]
        F --> G[Complete the subprocess]
        E -- The condition failed --> H[Error handling]
        H --> I[Keep a log]
        G --> J[end]
        I --> J

        style A fill:#ffe0b3,stroke:#ff9900,stroke-width:2px;
        style B fill:#ffcccc,stroke:#ff0000,stroke-width:2px;
        style C fill:#e6ffcc,stroke:#009933,stroke-width:2px;
        style D fill:#cce6ff,stroke:#0033cc,stroke-width:2px;
        style E fill:#ffccff,stroke:#9900cc,stroke-width:2px;
        style F fill:#ccccff,stroke:#3300cc,stroke-width:2px;
        style G fill:#b3ffff,stroke:#00cccc,stroke-width:2px;
        style H fill:#ffd9b3,stroke:#ff6600,stroke-width:2px;
        style I fill:#d9d9d9,stroke:#808080,stroke-width:2px;
        style J fill:#ccffcc,stroke:#009900,stroke-width:2px;

        linkStyle 0 stroke:#00cc00,stroke-width:2px;
        linkStyle 1 fill:#006600,stroke:#009933,stroke-width:2px,font-size:12px;
        linkStyle 2 fill:#990000,stroke:#ff3300,stroke-width:2px,font-size:12px;
        linkStyle 3 stroke:#ff33cc,stroke-width:2px;
        linkStyle 4 stroke:#cc33ff,stroke-width:2px;
        linkStyle 5 stroke:#33ccff,stroke-width:2px;
        linkStyle 6 stroke:#ff6600,stroke-width:2px,stroke-dasharray: 10,10;
        linkStyle 7 stroke:#999999,stroke-width:2px;
        linkStyle 8 stroke:#009900,stroke-width:2px;
        linkStyle 9 stroke:#ff6600,stroke-width:2px;
     """;

    private Dictionary<MermaidType, string> Diagrams { get; } = new Dictionary<MermaidType, string>
    {
        { MermaidType.None,
            """
            flowchart LR

            A[Hard] -->|Text| B(Round)
            B --> C{Decision}
            C -->|One| D[Result 1]
            C -->|Two| E[Result 2]
            """
        },
        { MermaidType.Flowchart,
            """
            A[Start] --> B{Is it working?}
            B -- Yes --> C[Keep going]
            B -- No --> D[Fix it]
            D --> B
            """
        },
        { MermaidType.SequenceDiagram,
            """
            participant Alice
            participant Bob
            Alice->>John: Hello John, how are you?
            loop HealthCheck
            John->>John: Fight against hypochondria
            end
            Note right of John: Rational thoughts <br/>prevail!
            John-->>Alice: Great!
            John->>Bob: How about you?
            Bob-->>John: Jolly good!
            """
        },
        { MermaidType.ClassDiagram,
            """
            Class01 <|-- AveryLongClass : Cool
            Class03 *-- Class04
            Class05 o-- Class06
            Class07 .. Class08
            Class09 --> C2 : Where am i?
            Class09 --* C3
            Class09 --|> Class07
            Class07 : equals()
            Class07 : Object[] elementData
            Class01 : size()
            Class01 : int chimp
            Class01 : int gorilla
            Class08 <--> C2: Cool label
            """
        },
        { MermaidType.StateDiagram,
            """
            [*] --> Still
            Still --> [*]

            Still --> Moving
            Moving --> Still
            Moving --> Crash
            Crash --> [*]
            """
        },
        { MermaidType.ErDiagram,
            """
            CUSTOMER ||--o{ ORDER : places
            ORDER ||--|{ LINE-ITEM : contains
            CUSTOMER }|..|{ DELIVERY-ADDRESS : uses
            """
        },
        { MermaidType.Journey,
            """
            section Go to work
            Make tea: 5: Me
            Go upstairs: 3: Me
            Do work: 1: Me, Cat
            section Go home
            Go downstairs: 5: Me
            Sit down: 5: Me
            """
        },
        { MermaidType.Gantt,
            """
            dateFormat  YYYY-MM-DD
            excludes weekdays 2014-01-10
                    
            section A section
            Completed task            :done,    des1, 2014-01-06,2014-01-08
            Active task               :active,  des2, 2014-01-09, 3d
            Future task               :         des3, after des2, 5d
            Future task2               :         des4, after des3, 5d
            """
        },
        { MermaidType.Pie,
            """
            "Dogs" : 386
            "Cats" : 85
            "Rats" : 15
            """
        }
    };

    private Task OnDownloadPDFAsync() => _mermaid.DownloadPdfAsync($"mermaid-pdf-{DateTime.Now:HHmmss}.pdf");

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>

    /// <summary>
    /// Methods
    /// </summary>
    /// <returns></returns>
    private MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = "ExportBase64MermaidAsync",
            Description = Localizer["ExportBase64Mermaid"],
            Parameters = " — ",
            ReturnValue = "string"
        }
    ];
}
