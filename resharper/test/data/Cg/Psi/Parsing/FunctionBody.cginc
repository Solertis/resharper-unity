{caret}
struct bar
{
    int zzz;
};

void foo()
{
    float a;
    float b = 0;
    float c = a;

    b = 0;
    b = a;

    bar bar_a;
    b = bar_a.zzz;    

    bar b;
    bar.zzz = 0;
    bar.zzz = bar.zzz;
}