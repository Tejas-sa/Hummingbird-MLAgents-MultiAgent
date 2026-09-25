# Hummingbird ML-Agents — Final Project Report

## 1. Overview
A Unity ML-Agents project using a PPO-trained Hummingbird policy, exported to ONNX and evaluated with five birds sharing one resource environment.

## 2. Architecture
Unity Environment → FlowerArea/resource control → 5 HummingbirdAgent inference instances → ResourceExperimentManager → CSV dataset → analysis/charts.

## 3. Experiment
The environment contains 20 flower resources. The experiment activates 20, 15, 10, or 5 flowers. Active flowers are randomized from the full resource set so scarcity does not always select the same spatial locations.

## 4. Official results

| Experiment | Birds | Flowers | Nectar | Duration (s) | Crowding events | Pair-time | Crowding ratio |
|---|---:|---:|---:|---:|---:|---:|---:|
| 5B_20F | 5 | 20 | 20.0004 | 80.38 | 10 | 169.60 | 21.10% |
| 5B_15F | 5 | 15 | 15.0003 | 63.38 | 20 | 168.01 | 26.51% |
| 5B_10F | 5 | 10 | 10.0001 | 66.33 | 21 | 208.73 | 31.47% |
| 5B_05F | 5 | 5 | 5.0000 | 26.36 | 5 | 50.01 | 18.97% |

## 5. Observations
Agents sometimes converge on the same flowers and become crowded. The frozen single-agent policy can also remain near depleted resource regions rather than learning a dedicated multi-agent search strategy.

Crowding ratio increased across the 20-, 15-, and 10-flower conditions (21.10%, 26.51%, 31.47%). The 5-flower condition measured 18.97%, so these four single trials do not establish a monotonic relationship.

## 6. Limitations
There is one completed trial per condition. The current data is therefore preliminary. The policy was not retrained with other-agent observations.

## 7. Future work
- Repeat each condition across multiple randomized trials.
- Record/fix random seeds.
- Train with other-agent observations.
- Add collision avoidance and exploration.
- Compare frozen single-agent and multi-agent-trained policies.

## 8. Portfolio description
Built a Unity ML-Agents multi-agent experiment using a PPO-trained Hummingbird policy. Extended a single-agent RL environment into a five-agent inference simulation with randomized resource scarcity, automated crowding metrics, and CSV-based experiment logging.
