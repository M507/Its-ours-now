# It's-ours-now

[![1](https://raw.githubusercontent.com/M507/its-ours-now/main/i.png)](https://raw.githubusercontent.com/M507/its-ours-now/main/i.png)

An excellent way to learn is by learning from others; I like to apply it by learning from other malware writers and analyzing their binaries/code, but many malware in the wild download separate files and then execute the downloaded files try to delete their tracks from the disk. A very fast way to catch their files before they get deleted is to wait for a creation event. Whenever an event gets executed, you can add an additional handler to do a set of instructions; and in this script's case, the handler backs up every created file. Their files are now.... our files. : ) 

