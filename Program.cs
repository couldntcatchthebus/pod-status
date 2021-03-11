using System;
using k8s;

namespace podStatus
{
    internal class PodListHealth
    {
        private static void Main(string[] args)
        {
            var config = KubernetesClientConfiguration.BuildDefaultConfig();
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var podHealth = client.ListNamespacedPod("default");
            foreach (var pod in podHealth.Items)
            {   
                if (pod.Status.Message != null)
                {
                    Console.WriteLine("************************");
                    Console.WriteLine("Pod name: " + pod.Metadata.Name);
                    Console.WriteLine("Pod status message: " + pod.Status.Message);
                    Console.WriteLine("Pod container statuses: " + pod.Status.ContainerStatuses);
                    Console.WriteLine("Pod status conditions: " + pod.Status.Conditions);
                    Console.WriteLine("Pod status reason: " + pod.Status.Reason);
                    Console.WriteLine("Pod status phase: "+ pod.Status.Phase);
                    Console.WriteLine("Pod start time: " + pod.Status.StartTime);
                    Console.WriteLine("************************");
                }
            }

            if (podHealth.Items.Count == 0)
            {
                Console.WriteLine("Empty!");
                Console.WriteLine("Check your namespace!");
            }
        }
    }
}