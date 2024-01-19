// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("/nRZEun9LPRp+O7F93/nEvtczqeYKqmKmKWuoYIu4C5fpampqa2oq8JDxnGWwdpJmH9rf4q9Ulc0BHM6ISu4RZ9QALserYVpyqypjKglyMnb9+AUUopyx3wR6yZ2Fg57DeYzrfBhrNQY6QnRNfFyAyTTizV4Xg8QjXWqw9gVbxabzyPxfK8Tld58CY9gvhIVRmbe6hu3jLVlyMEGzfKopyqpp6iYKqmiqiqpqahqmiRUoXrGuuRiYceqU4a2eQV2q1OH2kwFUskqSTNuBS9qryhN7F9SMuYTqa8IMWJwC54lE/wDAmfoab8JHTJqfRZpVfpzxDaOExW6SoGEJrNgxtT39hSBmquZeObwAdXh6ee+nzkBwBklG1icJzvJO8KwN6qrqaip");
        private static int[] order = new int[] { 6,6,3,9,4,5,7,11,11,10,11,13,13,13,14 };
        private static int key = 168;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
