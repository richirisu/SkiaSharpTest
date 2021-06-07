using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace SkiaSharpTest.iOS.Services
{
    public static class MachMemoryHelper
    {
        struct time_value_t
        {
            public /* integer_t */ int seconds;
            public /* integer_t */ int microseconds;
        }

        struct mach_task_basic_info
        {
            public /* mach_vm_size_t */ ulong virtual_size; /* virtual memory size (bytes) */
            public /* mach_vm_size_t */ ulong resident_size; /* resident memory size (bytes) */
            public /* mach_vm_size_t */ ulong resident_size_max; /* maximum resident memory size (bytes) */
            public /* time_value_t */ time_value_t user_time; /* total user run time for terminated threads */
            public /* time_value_t */ time_value_t system_time; /* total system run time for terminated threads */
            public /* policy_t */ int policy; /* default policy for new threads */
            public /* integer_t */ int suspend_count; /* suspend count for task */
        };

        [DllImport(Constants.SystemLibrary, EntryPoint = "task_info")]
        extern static /* kern_return_t */ int mach_task_info(
            /* task_name_t -> mach_port_t */ IntPtr target_task,
            /* task_flavor_t -> natural_t */ int flavor,
            /* task_info_t -> integer_t* */ ref mach_task_basic_info task_info_out,
            /* mach_msg_type_number_t* -> natural_t* */ ref int task_info_outCnt);

        [DllImport(Constants.SystemLibrary, EntryPoint = "mach_task_self")]
        extern static /* mach_port_t */ IntPtr mach_task_self();

        const int MACH_TASK_BASIC_INFO = 20; /* always 64-bit basic info */
        static int MACH_TASK_BASIC_INFO_COUNT = Marshal.SizeOf(typeof(mach_task_basic_info));

        static public long GetResidentSize()
        {
            mach_task_basic_info info = new mach_task_basic_info();
            mach_task_info(mach_task_self(), MACH_TASK_BASIC_INFO, ref info, ref MACH_TASK_BASIC_INFO_COUNT);
            return (long) info.resident_size;
        }

        static public long GetVirtualSize()
        {
            mach_task_basic_info info = new mach_task_basic_info();
            mach_task_info(mach_task_self(), MACH_TASK_BASIC_INFO, ref info, ref MACH_TASK_BASIC_INFO_COUNT);
            return (long) info.virtual_size;
        }
    }
}
