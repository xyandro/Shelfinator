OBJS = Banner.o BufferFile.o Controller.o DotStar.o Driver.o Helpers.o Lights.o Main.o Pattern.o Patterns.o Remote.o Timer.o

all: ../Build/shelfinator

../Build/shelfinator: $(OBJS)
	mkdir -p ../Build
	g++ -o $@ $^ -llirc_client -pthread

%.o: Shelfinator.Runner/%.cpp Shelfinator.Runner/%.h
	g++ -c -o $@ $<

clean:
	rm -f ../Build/shelfinator $(OBJS)
