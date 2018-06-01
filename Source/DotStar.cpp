#include "stdafx.h"
#include "DotStar.h"

namespace Shelfinator
{
	DotStar::DotStar(IDotStar ^dotStar) { this->dotStar = dotStar; }
	DotStar::ptr DotStar::Create(IDotStar ^dotStar) { return ptr(new DotStar(dotStar)); }
	void DotStar::Clear() { return dotStar->Clear(); }
	void DotStar::SetPixelColor(int led, int color) { return dotStar->SetPixelColor(led, color); }
	void DotStar::Show() { return dotStar->Show(); }
}
