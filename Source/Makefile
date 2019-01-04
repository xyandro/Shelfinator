OBJS = Audio.o Banner.o BufferFile.o Controller.o DotStar.o Driver.o Helpers.o Light.o LightColor.o LightItem.o Lights.o Main.o PaletteSequence.o PaletteSequences.o Remote.o Segment.o SegmentItem.o SegmentItems.o Song.o Songs.o Timer.o

all: ../Patterns/shelfinator

../Patterns/shelfinator: $(OBJS)
	mkdir -p ../Patterns
	g++ -o $@ $^ -lasound -llirc_client -lsndfile -pthread

%.o: Shelfinator.Runner/%.cpp Shelfinator.Runner/%.h
	g++ -c -o $@ $<

clean:
	rm -f ../Patterns/shelfinator $(OBJS)
