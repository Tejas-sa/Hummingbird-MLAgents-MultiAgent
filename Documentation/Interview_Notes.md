# Interview Notes

## 30-second explanation
I trained a Hummingbird reinforcement-learning agent with Unity ML-Agents using PPO, exported the policy to ONNX, and reused that frozen policy in a five-agent inference environment. I added randomized resource scarcity and built a C# experiment manager measuring nectar collection, completion time, and crowding behavior. The system automatically exported results to CSV and I analyzed them with Excel.

## Why PPO?
PPO was the training algorithm used for the Hummingbird policy.

## Why five copies of the same policy?
To create a controlled baseline for studying how a learned single-agent policy behaves when several copies operate in the same environment.

## Key limitation
The original policy was trained as a single-agent policy. It was not retrained specifically for cooperation, competition, or collision avoidance.

## What did you measure?
Nectar, flower depletion, duration, crowding events, pair-time, crowding ratio, and maximum simultaneous crowded pairs.

## Main observation
The 20-, 15-, and 10-flower trials measured 21.10%, 26.51%, and 31.47% crowding ratios; the 5-flower trial measured 18.97%. Because there is only one trial per condition, these are preliminary observations.

## Strong next step
Train a multi-agent-aware policy with other-agent observations, exploration, and collision avoidance, then compare it against this frozen-policy baseline.
