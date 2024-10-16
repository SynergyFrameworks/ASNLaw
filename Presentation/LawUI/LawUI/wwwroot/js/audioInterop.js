window.audioInterop = {
    mediaRecorder: null,
    recordedChunks: [],
    audioBlob: null,
    initialize: async function () {
        try {
            const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
            this.mediaRecorder = new MediaRecorder(stream);
            this.mediaRecorder.ondataavailable = (event) => {
                if (event.data.size > 0) {
                    this.recordedChunks.push(event.data);
                }
            };
            this.mediaRecorder.onstop = () => {
                this.audioBlob = new Blob(this.recordedChunks, { type: 'audio/webm' });
                this.recordedChunks = [];
            };
            console.log("Audio initialized successfully");
            return true;
        } catch (err) {
            console.error("Error initializing audio: " + err);
            return false;
        }
    },
    startRecording: function () {
        if (this.mediaRecorder && this.mediaRecorder.state === "inactive") {
            this.mediaRecorder.start();
            console.log("Recording started.");
            return true;
        }
        console.error("Cannot start recording. MediaRecorder not initialized or already recording.");
        return false;
    },
    stopRecording: function () {
        if (this.mediaRecorder && this.mediaRecorder.state === "recording") {
            this.mediaRecorder.stop();
            console.log("Recording stopped.");
            return true;
        }
        console.error("Cannot stop recording. MediaRecorder not recording.");
        return false;
    },
    playRecording: function () {
        if (this.audioBlob) {
            const audioURL = URL.createObjectURL(this.audioBlob);
            const audioPlayback = document.getElementById("audioPlayback");
            if (audioPlayback) {
                audioPlayback.src = audioURL;
                audioPlayback.play();
                return true;
            }
        }
        console.error("No audio recording available to play.");
        return false;
    }
};