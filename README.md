# 🐦 Hummingbird Multi-Agent Reinforcement Learning

> **A Unity ML-Agents experiment investigating how a frozen PPO-trained Hummingbird policy behaves when replicated across multiple agents sharing limited spatial resources.**

![Unity](https://img.shields.io/badge/Unity-6-black?logo=unity)
![ML-Agents](https://img.shields.io/badge/Unity%20ML--Agents-Reinforcement%20Learning-blue)
![C%23](https://img.shields.io/badge/C%23-Scripting-purple?logo=csharp)
![PPO](https://img.shields.io/badge/Algorithm-PPO-orange)
![ONNX](https://img.shields.io/badge/Inference-ONNX-green)

---

## 🎯 Project Overview

This project started with a reinforcement-learning Hummingbird environment built using **Unity ML-Agents**.

After training a PPO policy, I extended the environment into a controlled **multi-agent inference experiment**:

```text
                 PPO Training
                     │
                     ▼
              Trained Hummingbird
                     │
                     ▼
                 ONNX Model
                     │
                     ▼
          ┌──────────────────────┐
          │  5 Hummingbird       │
          │  inference agents    │
          └──────────┬───────────┘
                     │
                     ▼
          Shared Resource World
                     │
             ┌───────┴────────┐
             │                │
             ▼                ▼
       Flower scarcity    Agent interaction
       20 / 15 / 10 / 5    & crowding
             │                │
             └───────┬────────┘
                     ▼
          Experiment Manager
                     │
                     ▼
              CSV Dataset
                     │
                     ▼
           Quantitative Analysis
```

The goal was not simply to observe the birds visually, but to **instrument the environment and measure their behavior quantitatively**.

---

## 🧠 Research Question

> **How does a frozen single-agent reinforcement-learning policy behave when multiple copies of the policy operate simultaneously in a shared environment with different levels of resource availability?**

The experiment focuses on:

- resource consumption
- agent convergence
- crowding
- completion time
- resource scarcity

---

## 🤖 Reinforcement Learning

The Hummingbird policy was trained using:

- **Unity ML-Agents**
- **PPO (Proximal Policy Optimization)**
- Continuous action space
- 10 vector observations
- 5 continuous actions
- ONNX model for inference

The trained policy was then transferred into the multi-agent experiment.

### Important design decision

All five birds use the **same frozen ONNX policy**.

This creates a controlled baseline for studying how a learned single-agent policy behaves when several copies operate in the same environment.

> **Important limitation:** the original policy was trained in a single-agent environment. The five-agent experiment therefore evaluates a frozen single-agent policy under multi-agent interaction; it is not a policy trained specifically for cooperation or competition.

---

# 🌸 Resource Experiment

The experimental island contains **20 flower resources**.

For each experiment, the system randomly selects which flowers are available.

### Conditions

| Experiment | Birds | Active Flowers |
|---|---:|---:|
| `5B_20F` | 5 | 20 |
| `5B_15F` | 5 | 15 |
| `5B_10F` | 5 | 10 |
| `5B_05F` | 5 | 5 |

The number of birds remains constant while resource availability changes.

This gives the experiment a controlled independent variable:

> **Active flower count**

---

# 📊 Behavioral Metrics

The custom `ResourceExperimentManager` records:

| Metric | Description |
|---|---|
| Nectar Collected | Total nectar consumed by the agents |
| Flowers Depleted | Number of resources completely consumed |
| Duration | Time required to complete the experiment |
| Crowding Events | Number of detected close-proximity events |
| Crowding Pair-Time | Total time accumulated by crowded agent pairs |
| Crowding Ratio | Crowded pair-time relative to total pair-time |
| Max Simultaneous Crowded Pairs | Maximum number of pairs crowded at once |

Crowding was measured using a configurable distance threshold of:

```text
0.50 Unity units
```

---

# 📈 Experimental Results

The current dataset contains **one completed randomized trial per condition**.

| Condition | Nectar | Duration | Crowding Events | Pair-Time | Crowding Ratio |
|---|---:|---:|---:|---:|---:|
| 20 flowers | 20.0004 | 80.38 s | 10 | 169.60 | 21.10% |
| 15 flowers | 15.0003 | 63.38 s | 20 | 168.01 | 26.51% |
| 10 flowers | 10.0001 | 66.33 s | 21 | 208.73 | 31.47% |
| 5 flowers | 5.0000 | 26.36 s | 5 | 50.01 | 18.97% |

### Observed behavior

The measured crowding ratio increased across the 20-, 15-, and 10-flower conditions:

```text
21.10% → 26.51% → 31.47%
```

However, the 5-flower condition measured:

```text
18.97%
```

Therefore, the current four single-trial results **do not establish a monotonic relationship** between resource availability and crowding.

The data should be treated as a **preliminary behavioral experiment**, not a statistically conclusive study.

---

# 🐦 Multi-Agent Behavior

During testing, several behaviors became visible:

### Resource convergence

Multiple birds can converge toward the same flower even though they are independent copies of the same policy.

### Spatial crowding

When several birds target nearby resources, they can enter the defined crowding distance.

### Policy limitation

After available resources are depleted, birds can remain around the final resource region instead of performing a learned global search strategy.

This behavior is an important consequence of evaluating a policy outside the exact single-agent training distribution.

---

# 🧩 Technical Architecture

```text
Unity Scene
│
├── FloatingIsland
│   ├── FlowerArea
│   │   ├── Flower resources
│   │   └── Resource randomization
│   │
│   └── Hummingbird × 5
│       └── Shared ONNX policy
│
├── ResourceExperimentManager
│   ├── Experiment configuration
│   ├── Nectar statistics
│   ├── Flower depletion
│   ├── Duration measurement
│   ├── Crowding detection
│   └── CSV export
│
└── Experimental Dataset
    ├── Raw CSV
    ├── Clean CSV
    └── Analysis / Charts
```

---

# 🔬 Experimental Method

### Step 1 — Train

Train the Hummingbird policy using PPO in Unity ML-Agents.

### Step 2 — Export

Export the trained policy to an ONNX model.

### Step 3 — Inference

Run multiple Hummingbird agents using the same frozen policy.

### Step 4 — Resource control

Control the number of active flowers:

```text
20 → 15 → 10 → 5
```

### Step 5 — Randomization

Randomize which flowers are active so that resource scarcity does not always select the same spatial positions.

### Step 6 — Measurement

Record:

- resource consumption
- completion time
- crowding
- pair-time
- crowding ratio

### Step 7 — Data export

Automatically save experimental results to CSV.

### Step 8 — Analysis

Analyze the recorded dataset using spreadsheet-based analysis and generated charts.

---

# 📁 Repository Structure

Recommended GitHub structure:

```text
Hummingbird-MLAgents-MultiAgent/
│
├── README.md
│
├── Assets/
│   └── ...
│
├── Packages/
│   └── manifest.json
│
├── ProjectSettings/
│   └── ...
│
├── Documentation/
│   ├── Project_Report.md
│   ├── Experiment_Protocol.md
│   └── Interview_Notes.md
│
├── Data/
│   ├── official_results.csv
│   └── raw_results.csv
│
├── Analysis/
│   ├── experiment_analysis.xlsx
│   └── charts/
│
├── Media/
│   ├── screenshots/
│   └── video/
│
└── .gitignore
```

### Do not upload generated Unity folders

The repository should normally exclude:

```text
Library/
Temp/
Logs/
Obj/
Build/
UserSettings/
```

The original Unity project should retain:

```text
Assets/
Packages/
ProjectSettings/
```

---

# 🛠️ Technologies

- **Unity 6**
- **Unity ML-Agents**
- **C#**
- **PPO**
- **ONNX**
- **Reinforcement Learning**
- **Multi-Agent Simulation**
- **CSV Data Collection**
- **Experimental Data Analysis**

---

# 💡 What I Learned

This project helped explore the difference between:

### Single-agent learning

A policy learns how one agent behaves in an environment.

### Multi-agent deployment

Multiple copies of that policy can interact in ways that were not necessarily represented during training.

This highlighted the importance of:

- observation design
- reward design
- multi-agent training
- collision avoidance
- resource allocation
- behavioral measurement
- experimental methodology

---

# 🚀 Future Work

The next version can extend the experiment in several directions.

### 1. Multi-agent-aware training

Add observations representing nearby agents and retrain the policy.

### 2. Collision avoidance

Introduce explicit penalties or steering behavior for excessive agent proximity.

### 3. Cooperative behavior

Test whether agents can learn to distribute themselves across resources.

### 4. Repeated experiments

Run multiple randomized trials for every resource condition and report:

```text
Mean ± variation
```

instead of relying on a single trial.

### 5. Reproducibility

Record random seeds for each experiment.

### 6. Policy comparison

Compare:

```text
Frozen single-agent policy
            vs
Multi-agent-trained policy
```

This would provide a stronger evaluation of multi-agent learning.

---

# 🎮 Portfolio Positioning

This project can be presented as a combination of:

```text
Game AI
   +
Reinforcement Learning
   +
Multi-Agent Systems
   +
Data Science
   +
Behavioral Simulation
```

### Portfolio description

> Built a Unity ML-Agents multi-agent experiment using a PPO-trained Hummingbird policy. Extended a single-agent reinforcement-learning environment into a five-agent inference simulation with randomized resource scarcity, automated crowding metrics, and CSV-based experiment logging. Analyzed resource consumption, completion time, crowding events, pair-time, and crowding ratio across four resource conditions.

---

# 📌 Project Status

### Core implementation

**Complete ✅**

### Experimental dataset

**Preliminary 🟡**

### Future research

**Multi-agent-aware training + repeated randomized trials**

---

## 📄 Documentation

- [Project Report](Documentation/Project_Report.md)
- [Experiment Protocol](Documentation/Experiment_Protocol.md)
- [Interview Notes](Documentation/Interview_Notes.md)

---

## 👤 Author

**Tejas Gambhire**

Game Development • AI • Reinforcement Learning • Technical Art

---

> **This project is an experimental exploration of how learned game-AI policies behave when deployed beyond their original single-agent training setting.**
