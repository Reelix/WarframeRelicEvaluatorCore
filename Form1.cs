using Gma.System.MouseKeyHook;
using Newtonsoft.Json; // I know I know...
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tesseract;

namespace WarframeRelicEvaluatorCore
{
    public partial class Form1 : Form
    {
        // https://docs.google.com/spreadsheets/d/1AaTziJzt35bifF5kwrap003cuPqFwy0FnV6i2sBW7ho/edit
        static string warframeItems = "";
        static string username = Environment.UserName;
        // static string warframeItems = "";// Akstiletto Prime Blueprint,Saryn Prime Neuroptics Blueprint,Vectis Prime Blueprint,Braton Prime Barrel,Vasto Prime Receiver,Forma Blueprint,Akstiletto Prime Blueprint,Akbronco Prime Link,Cernos Prime Blueprint,Lex Prime Barrel,Valkyr Prime Blueprint,Forma Blueprint,Akjagara Prime Barrel,Akbolto Prime Barrel,Tiberon Prime Blueprint,Lex Prime Blueprint,Orthos Prime Handle,Rubico Prime Receiver,Boar Prime Stock,Boltor Prime Receiver,Rhino Prime Neuroptics Blueprint,Ankyros Prime Blueprint,Mag Prime Neuroptics Blueprint,Forma Blueprint,Ballistica Prime Blueprint,Braton Prime Receiver,Orthos Prime Blade,Paris Prime Lower Limb,Tigris Prime Stock,Forma Blueprint,Ballistica Prime Blueprint,Cernos Prime String,Orthos Prime Blade,Burston Prime Blueprint,Limbo Prime Systems Blueprint,Forma Blueprint,Boltor Prime Blueprint,Rhino Prime Neuroptics Blueprint,Scindo Prime Blueprint,Ankyros Prime Blueprint,Nyx Prime Blueprint,Forma Blueprint,Ballistica Prime Blueprint,Orthos Prime Blade,Rubico Prime Stock,Banshee Prime Neuroptics Blueprint,Lex Prime Receiver,Oberon Prime Chassis Blueprint,Ballistica Prime Blueprint,Orthos Prime Blueprint,Redeemer Prime Blueprint,Fang Prime Blueprint,Lex Prime Barrel,Limbo Prime Systems Blueprint,Carrier Prime Cerebrum,Kavasa Prime Band,Forma Blueprint,Fang Prime Blade,Nova Prime Neuroptics Blueprint,Nova Prime Systems Blueprint,Cernos Prime Lower Limb,Akbolto Prime Barrel,Nami Skyla Prime Handle,Bronco Prime Receiver,Paris Prime Upper Limb,Venka Prime Blades,Chroma Prime Neuroptics Blueprint,Braton Prime Blueprint,Helios Prime Blueprint,Lex Prime Blueprint,Silva & Aegis Prime Hilt,Sybaris Prime Blueprint,Chroma Prime Neuroptics Blueprint,Braton Prime Stock,Orthos Prime Blade,Burston Prime Receiver,Paris Prime Lower Limb,Forma Blueprint,Fragor Prime Blueprint,Odonata Prime Blueprint,Scindo Prime Blueprint,Fang Prime Blade,Paris Prime Blueprint,Forma Blueprint,Vauban Prime Blueprint,Burston Prime Barrel,Fang Prime Handle,Hikou Prime Stars,Odonata Prime Systems Blueprint,Paris Prime Upper Limb,Glaive Prime Blueprint,Ember Prime Systems Blueprint,Forma Blueprint,Frost Prime Chassis Blueprint,Latron Prime Blueprint,Reaper Prime Handle,Glaive Prime Blueprint,Frost Prime Systems Blueprint,Loki Prime Chassis Blueprint,Bo Prime Blueprint,Latron Prime Stock,Wyrm Prime Cerebrum,Helios Prime Cerebrum,Akstiletto Prime Barrel,Nami Skyla Prime Handle,Bronco Prime Blueprint,Paris Prime Blueprint,Saryn Prime Systems Blueprint,Helios Prime Cerebrum,Oberon Prime Blueprint,Orthos Prime Blueprint,Lex Prime Barrel,Zephyr Prime Neuroptics Blueprint,Forma Blueprint,Kavasa Prime Buckle,Tigris Prime Barrel,Trinity Prime Blueprint,Burston Prime Stock,Fang Prime Blueprint,Odonata Prime Harness Blueprint,Kogake Prime Gauntlet,Braton Prime Receiver,Forma Blueprint,Braton Prime Barrel,Rubico Prime Receiver,Zephyr Prime Neuroptics Blueprint,Mag Prime Blueprint,Dakra Prime Blueprint,Soma Prime Blueprint,Boar Prime Receiver,Lex Prime Receiver,Forma Blueprint,Mag Prime Blueprint,Boar Prime Barrel,Forma Blueprint,Dakra Prime Handle,Nova Prime Neuroptics Blueprint,Soma Prime Barrel,Mesa Prime Neuroptics Blueprint,Oberon Prime Blueprint,Rubico Prime Stock,Paris Prime Blueprint,Silva & Aegis Prime Hilt,Forma Blueprint,Mesa Prime Neuroptics Blueprint,Destreza Prime Blueprint,Stradavar Prime Barrel,Akjagara Prime Blueprint,Equinox Prime Chassis Blueprint,Paris Prime Blueprint,Nova Prime Chassis Blueprint,Akbronco Prime Link,Forma Blueprint,Bronco Prime Blueprint,Fang Prime Blade,Nekros Prime Chassis Blueprint,Nekros Prime Systems Blueprint,Helios Prime Blueprint,Kavasa Prime Band,Carrier Prime Systems,Paris Prime Upper Limb,Spira Prime Blueprint,Nekros Prime Blueprint,Braton Prime Blueprint,Forma Blueprint,Burston Prime Receiver,Sybaris Prime Stock,Valkyr Prime Blueprint,Oberon Prime Neuroptics Blueprint,Akbolto Prime Barrel,Forma Blueprint,Ballistica Prime Lower Limb,Burston Prime Blueprint,Rubico Prime Receiver,Odonata Prime Wings Blueprint,Loki Prime Chassis Blueprint,Forma Blueprint,Bo Prime Blueprint,Volt Prime Blueprint,Wyrm Prime Cerebrum,Pyrana Prime Blueprint,Braton Prime Receiver,Forma Blueprint,Fang Prime Blade,Helios Prime Carapace,Sybaris Prime Stock,Pyrana Prime Blueprint,Ballistica Prime Receiver,Gram Prime Blueprint,Burston Prime Blueprint,Tipedo Prime Blueprint,Forma Blueprint,Spira Prime Pouch,Paris Prime Grip,Forma Blueprint,Bronco Prime Blueprint,Hikou Prime Blueprint,Paris Prime String,Soma Prime Stock,Kavasa Prime Band,Forma Blueprint,Akbronco Prime Blueprint,Carrier Prime Systems,Nyx Prime Blueprint,Spira Prime Pouch,Ash Prime Blueprint,Soma Prime Receiver,Akbronco Prime Blueprint,Carrier Prime Systems,Paris Prime String,Saryn Prime Blueprint,Kavasa Prime Kubrow Collar Blueprint,Trinity Prime Chassis Blueprint,Lex Prime Blueprint,Paris Prime Blueprint,Forma Blueprint,Spira Prime Blade,Akbronco Prime Link,Galatine Prime Handle,Burston Prime Receiver,Carrier Prime Carapace,Fang Prime Blade,Spira Prime Pouch,Kavasa Prime Band,Valkyr Prime Neuroptics Blueprint,Dual Kamas Prime Blueprint,Fang Prime Blade,Forma Blueprint,Sybaris Prime Barrel,Akbolto Prime Link,Paris Prime Grip,Galatine Prime Blade,Lex Prime Blueprint,Mirage Prime Chassis Blueprint,Tigris Prime Blueprint,Ballistica Prime Upper Limb,Valkyr Prime Neuroptics Blueprint,Akbronco Prime Blueprint,Burston Prime Blueprint,Forma Blueprint,Tiberon Prime Stock,Braton Prime Blueprint,Silva & Aegis Prime Blueprint,Lex Prime Barrel,Valkyr Prime Blueprint,Forma Blueprint,Fragor Prime Handle,Volt Prime Systems Blueprint,Forma Blueprint,Burston Prime Stock,Paris Prime Lower Limb,Paris Prime Upper Limb,Vauban Prime Systems Blueprint,Paris Prime Upper Limb,Forma Blueprint,Lex Prime Barrel,Fang Prime Blueprint,Paris Prime Lower Limb,Valkyr Prime Systems Blueprint,Helios Prime Systems,Tigris Prime Barrel,Braton Prime Blueprint,Cernos Prime Upper Limb,Paris Prime Lower Limb,Vauban Prime Chassis Blueprint,Helios Prime Blueprint,Tigris Prime Barrel,Nekros Prime Chassis Blueprint,Spira Prime Blueprint,Venka Prime Blades,Valkyr Prime Systems Blueprint,Akbolto Prime Barrel,Ballistica Prime Upper Limb,Akbronco Prime Blueprint,Banshee Prime Neuroptics Blueprint,Lex Prime Blueprint,Zephyr Prime Systems Blueprint,Cernos Prime Blueprint,Forma Blueprint,Akbronco Prime Blueprint,Fang Prime Handle,Mirage Prime Neuroptics Blueprint,Zephyr Prime Systems Blueprint,Helios Prime Systems,Silva & Aegis Prime Blueprint,Akbronco Prime Blueprint,Nami Skyla Prime Blueprint,Forma Blueprint,Akjagara Prime Barrel,Kronen Prime Handle,Forma Blueprint,Ballistica Prime Lower Limb,Orthos Prime Handle,Sybaris Prime Stock,Boar Prime Stock,Mag Prime Chassis Blueprint,Orthos Prime Blueprint,Dakra Prime Handle,Orthos Prime Handle,Forma Blueprint,Banshee Prime Chassis Blueprint,Hydroid Prime Blueprint,Kronen Prime Handle,Braton Prime Blueprint,Oberon Prime Chassis Blueprint,Silva & Aegis Prime Hilt,Boar Prime Stock,Dakra Prime Blueprint,Mag Prime Chassis Blueprint,Nova Prime Systems Blueprint,Soma Prime Blueprint,Forma Blueprint,Carrier Prime Cerebrum,Ash Prime Blueprint,Scindo Prime Handle,Nova Prime Neuroptics Blueprint,Saryn Prime Systems Blueprint,Forma Blueprint,Carrier Prime Cerebrum,Odonata Prime Blueprint,Valkyr Prime Neuroptics Blueprint,Bronco Prime Blueprint,Cernos Prime Upper Limb,Galatine Prime Blade,Cernos Prime Lower Limb,Helios Prime Blueprint,Saryn Prime Blueprint,Burston Prime Stock,Lex Prime Blueprint,Nami Skyla Prime Blueprint,Dual Kamas Prime Blade,Orthos Prime Blade,Forma Blueprint,Lex Prime Blueprint,Orthos Prime Handle,Forma Blueprint,Destreza Prime Blade,Helios Prime Systems,Valkyr Prime Neuroptics Blueprint,Lex Prime Receiver,Paris Prime Lower Limb,Forma Blueprint,Destreza Prime Blade,Kronen Prime Handle,Nami Skyla Prime Handle,Akbolto Prime Blueprint,Braton Prime Stock,Fang Prime Handle,Ember Prime Blueprint,Wyrm Prime Systems,Forma Blueprint,Bo Prime Ornament,Frost Prime Chassis Blueprint,Latron Prime Barrel,Equinox Prime Systems Blueprint,Akbolto Prime Link,Rubico Prime Stock,Bronco Prime Blueprint,Destreza Prime Handle,Pyrana Prime Barrel,Fragor Prime Blueprint,Dual Kamas Prime Handle,Nekros Prime Neuroptics Blueprint,Bronco Prime Receiver,Paris Prime Lower Limb,Saryn Prime Systems Blueprint,Frost Prime Blueprint,Reaper Prime Blade,Forma Blueprint,Ember Prime Chassis Blueprint,Latron Prime Stock,Sicarus Prime Barrel,Frost Prime Blueprint,Glaive Prime Disc,Forma Blueprint,Ember Prime Neuroptics Blueprint,Loki Prime Blueprint,Reaper Prime Handle,Galatine Prime Blueprint,Ballistica Prime String,Silva & Aegis Prime Blade,Braton Prime Barrel,Venka Prime Blades,Forma Blueprint,Helios Prime Cerebrum,Fragor Prime Blueprint,Oberon Prime Blueprint,Hydroid Prime Chassis Blueprint,Mirage Prime Neuroptics Blueprint,Paris Prime Blueprint,Kogake Prime Gauntlet,Akstiletto Prime Barrel,Tigris Prime Receiver,Cernos Prime Upper Limb,Nekros Prime Chassis Blueprint,Orthos Prime Handle,Mag Prime Blueprint,Boar Prime Barrel,Rhino Prime Chassis Blueprint,Ankyros Prime Gauntlet,Boltor Prime Barrel,Dakra Prime Handle,Mirage Prime Blueprint,Akjagara Prime Link,Silva & Aegis Prime Blade,Fang Prime Blade,Kogake Prime Boot,Paris Prime String,Mirage Prime Blueprint,Equinox Prime Neuroptics Blueprint,Kogake Prime Blueprint,Ballistica Prime Lower Limb,Gram Prime Blade,Paris Prime String,Nyx Prime Neuroptics Blueprint,Dual Kamas Prime Handle,Forma Blueprint,Braton Prime Stock,Fang Prime Blueprint,Fang Prime Handle,Vauban Prime Neuroptics Blueprint,Ash Prime Neuroptics Blueprint,Forma Blueprint,Bronco Prime Blueprint,Fang Prime Handle,Hikou Prime Blueprint,Nekros Prime Blueprint,Carrier Prime Cerebrum,Euphona Prime Barrel,Braton Prime Barrel,Bronco Prime Blueprint,Burston Prime Stock,Nikana Prime Blade,Oberon Prime Blueprint,Forma Blueprint,Orthos Prime Handle,Paris Prime Upper Limb,Saryn Prime Systems Blueprint,Nekros Prime Systems Blueprint,Hydroid Prime Blueprint,Tigris Prime Receiver,Braton Prime Stock,Lex Prime Receiver,Forma Blueprint,Nyx Prime Neuroptics Blueprint,Rhino Prime Chassis Blueprint,Scindo Prime Handle,Boltor Prime Barrel,Hikou Prime Pouch,Forma Blueprint,Oberon Prime Neuroptics Blueprint,Akstiletto Prime Link,Cernos Prime String,Akbronco Prime Blueprint,Paris Prime Lower Limb,Forma Blueprint,Oberon Prime Neuroptics Blueprint,Akbronco Prime Link,Mirage Prime Systems Blueprint,Cernos Prime Upper Limb,Paris Prime Lower Limb,Tiberon Prime Receiver,Odonata Prime Wings Blueprint,Volt Prime Chassis Blueprint,Wyrm Prime Systems,Loki Prime Neuroptics Blueprint,Odonata Prime Systems Blueprint,Forma Blueprint,Pyrana Prime Blueprint,Ballistica Prime Receiver,Silva & Aegis Prime Blueprint,Bronco Prime Receiver,Chroma Prime Blueprint,Oberon Prime Chassis Blueprint,Rubico Prime Blueprint,Akbronco Prime Link,Ballistica Prime String,Braton Prime Blueprint,Paris Prime Upper Limb,Tiberon Prime Receiver,Soma Prime Stock,Odonata Prime Blueprint,Paris Prime Upper Limb,Nova Prime Systems Blueprint,Paris Prime Blueprint,Tigris Prime Stock,Saryn Prime Chassis Blueprint,Paris Prime Grip,Forma Blueprint,Bronco Prime Blueprint,Galatine Prime Blade,Paris Prime String,Spira Prime Pouch,Ash Prime Blueprint,Forma Blueprint,Akbronco Prime Blueprint,Nekros Prime Chassis Blueprint,Valkyr Prime Blueprint,Saryn Prime Blueprint,Fang Prime Handle,Trinity Prime Blueprint,Banshee Prime Neuroptics Blueprint,Fang Prime Blueprint,Galatine Prime Blade,Sybaris Prime Barrel,Saryn Prime Neuroptics Blueprint,Forma Blueprint,Braton Prime Barrel,Burston Prime Stock,Lex Prime Barrel,Spira Prime Blade,Cernos Prime String,Tigris Prime Receiver,Galatine Prime Blade,Helios Prime Carapace,Valkyr Prime Blueprint,Sybaris Prime Barrel,Nami Skyla Prime Handle,Paris Prime Grip,Cernos Prime Upper Limb,Venka Prime Blades,Forma Blueprint,Sybaris Prime Barrel,Ballistica Prime Upper Limb,Burston Prime Barrel,Bronco Prime Receiver,Gram Prime Blade,Forma Blueprint,Spira Prime Pouch,Saryn Prime Neuroptics Blueprint,Forma Blueprint,Braton Prime Blueprint,Lex Prime Barrel,Spira Prime Blueprint,Tiberon Prime Barrel,Paris Prime Upper Limb,Forma Blueprint,Braton Prime Stock,Burston Prime Receiver,Fang Prime Blueprint,Tiberon Prime Stock,Banshee Prime Blueprint,Tigris Prime Receiver,Lex Prime Barrel,Paris Prime Lower Limb,Forma Blueprint,Tiberon Prime Stock,Chroma Prime Chassis Blueprint,Limbo Prime Blueprint,Lex Prime Barrel,Paris Prime Lower Limb,Forma Blueprint,Vectis Prime Receiver,Braton Prime Blueprint,Forma Blueprint,Burston Prime Blueprint,Carrier Prime Blueprint,Spira Prime Blueprint,Nikana Prime Blade,Volt Prime Chassis Blueprint,Forma Blueprint,Ash Prime Chassis Blueprint,Paris Prime Blueprint,Vasto Prime Receiver,Valkyr Prime Systems Blueprint,Kavasa Prime Band,Volt Prime Chassis Blueprint,Carrier Prime Blueprint,Spira Prime Blueprint,Forma Blueprint,Vauban Prime Chassis Blueprint,Ash Prime Neuroptics Blueprint,Valkyr Prime Neuroptics Blueprint,Carrier Prime Blueprint,Lex Prime Barrel,Forma Blueprint,Venka Prime Gauntlet,Silva & Aegis Prime Blueprint,Euphona Prime Barrel,Bronco Prime Blueprint,Cernos Prime Grip,Spira Prime Blueprint,Zephyr Prime Blueprint,Helios Prime Blueprint,Limbo Prime Blueprint,Burston Prime Receiver,Fang Prime Blueprint,Forma Blueprint,Zephyr Prime Systems Blueprint,Hydroid Prime Blueprint,Rubico Prime Barrel,Braton Prime Blueprint,Redeemer Prime Blade,Sybaris Prime Blueprint,Zephyr Prime Systems Blueprint,Akjagara Prime Link,Rubico Prime Barrel,Chroma Prime Blueprint,Stradavar Prime Receiver,Forma Blueprint,Akstiletto Prime Blueprint,Cernos Prime String,Vectis Prime Blueprint,Carrier Prime Systems,Braton Prime Barrel,Forma Blueprint,Akbolto Prime Receiver,Zephyr Prime Chassis Blueprint,Forma Blueprint,Kronen Prime Blueprint,Limbo Prime Systems Blueprint,Pyrana Prime Barrel,Banshee Prime Chassis Blueprint,Burston Prime Barrel,Vectis Prime Stock,Bronco Prime Receiver,Paris Prime String,Trinity Prime Systems Blueprint,Banshee Prime Chassis Blueprint,Venka Prime Blueprint,Forma Blueprint,Braton Prime Stock,Fang Prime Blueprint,Tigris Prime Stock,Boltor Prime Blueprint,Dakra Prime Blade,Mag Prime Chassis Blueprint,Boar Prime Blueprint,Rhino Prime Systems Blueprint,Forma Blueprint,Banshee Prime Systems Blueprint,Kronen Prime Handle,Tigris Prime Barrel,Burston Prime Stock,Hydroid Prime Chassis Blueprint,Forma Blueprint,Banshee Prime Systems Blueprint,Destreza Prime Blueprint,Oberon Prime Blueprint,Ballistica Prime Lower Limb,Braton Prime Barrel,Forma Blueprint,Chroma Prime Systems Blueprint,Pyrana Prime Receiver,Redeemer Prime Blueprint,Kronen Prime Blueprint,Limbo Prime Systems Blueprint,Mesa Prime Chassis Blueprint,Dakra Prime Blade,Trinity Prime Neuroptics Blueprint,Forma Blueprint,Boar Prime Blueprint,Mag Prime Systems Blueprint,Vasto Prime Barrel,Ember Prime Blueprint,Reaper Prime Blade,Forma Blueprint,Frost Prime Neuroptics Blueprint,Loki Prime Neuroptics Blueprint,Wyrm Prime Blueprint,Frost Prime Blueprint,Bo Prime Handle,Forma Blueprint,Ember Prime Chassis Blueprint,Sicarus Prime Barrel,Sicarus Prime Blueprint,Gram Prime Handle,Hydroid Prime Neuroptics Blueprint,Paris Prime Grip,Mirage Prime Neuroptics Blueprint,Kogake Prime Boot,Forma Blueprint,Hydroid Prime Systems Blueprint,Banshee Prime Blueprint,Tigris Prime Barrel,Burston Prime Receiver,Fragor Prime Head,Orthos Prime Handle,Hydroid Prime Systems Blueprint,Ballistica Prime Receiver,Rubico Prime Barrel,Burston Prime Stock,Euphona Prime Blueprint,Fang Prime Blueprint,Kogake Prime Gauntlet,Helios Prime Blueprint,Zephyr Prime Chassis Blueprint,Euphona Prime Blueprint,Silva & Aegis Prime Hilt,Valkyr Prime Blueprint,Kogake Prime Gauntlet,Akbolto Prime Link,Mirage Prime Systems Blueprint,Chroma Prime Blueprint,Banshee Prime Neuroptics Blueprint,Tiberon Prime Receiver,Limbo Prime Chassis Blueprint,Ballistica Prime Receiver,Silva & Aegis Prime Blade,Euphona Prime Blueprint,Kronen Prime Blueprint,Mirage Prime Chassis Blueprint,Mirage Prime Blueprint,Euphona Prime Barrel,Helios Prime Blueprint,Akbolto Prime Blueprint,Bronco Prime Blueprint,Paris Prime String,Mesa Prime Systems Blueprint,Bronco Prime Barrel,Limbo Prime Blueprint,Akbronco Prime Blueprint,Burston Prime Stock,Forma Blueprint,Nyx Prime Chassis Blueprint,Kavasa Prime Kubrow Collar Blueprint,Soma Prime Receiver,Hikou Prime Stars,Vectis Prime Barrel,Forma Blueprint,Vauban Prime Systems Blueprint,Nova Prime Blueprint,Forma Blueprint,Fang Prime Blueprint,Lex Prime Blueprint,Vasto Prime Barrel,Nekros Prime Systems Blueprint,Ash Prime Neuroptics Blueprint,Fang Prime Handle,Lex Prime Barrel,Odonata Prime Systems Blueprint,Paris Prime String,Nikana Prime Blade,Venka Prime Blueprint,Forma Blueprint,Ash Prime Chassis Blueprint,Braton Prime Stock,Paris Prime String,Nikana Prime Blueprint,Akstiletto Prime Link,Ash Prime Systems Blueprint,Helios Prime Carapace,Saryn Prime Systems Blueprint,Forma Blueprint,Nikana Prime Hilt,Sybaris Prime Receiver,Trinity Prime Blueprint,Bronco Prime Receiver,Cernos Prime Upper Limb,Galatine Prime Blade,Nekros Prime Systems Blueprint,Banshee Prime Blueprint,Valkyr Prime Neuroptics Blueprint,Euphona Prime Blueprint,Trinity Prime Neuroptics Blueprint,Venka Prime Blades,Nami Skyla Prime Blade,Akbolto Prime Link,Forma Blueprint,Hydroid Prime Chassis Blueprint,Paris Prime String,Sybaris Prime Blueprint,Nova Prime Chassis Blueprint,Soma Prime Receiver,Forma Blueprint,Boar Prime Blueprint,Dakra Prime Handle,Mag Prime Neuroptics Blueprint,Nami Skyla Prime Blade,Ballistica Prime Upper Limb,Tipedo Prime Ornament,Redeemer Prime Blade,Zephyr Prime Neuroptics Blueprint,Forma Blueprint,Odonata Prime Wings Blueprint,Aklex Prime Blueprint,Volt Prime Systems Blueprint,Odonata Prime Harness Blueprint,Volt Prime Blueprint,Forma Blueprint,Rhino Prime Blueprint,Boltor Prime Receiver,Nyx Prime Chassis Blueprint,Ankyros Prime Gauntlet,Hikou Prime Blueprint,Forma Blueprint,Saryn Prime Blueprint,Trinity Prime Chassis Blueprint,Forma Blueprint,Carrier Prime Carapace,Lex Prime Barrel,Soma Prime Blueprint,Saryn Prime Chassis Blueprint,Nyx Prime Systems Blueprint,Paris Prime Upper Limb,Burston Prime Stock,Nova Prime Systems Blueprint,Paris Prime Lower Limb,Spira Prime Blade,Vasto Prime Blueprint,Forma Blueprint,Burston Prime Receiver,Carrier Prime Blueprint,Soma Prime Barrel,Sicarus Prime Receiver,Frost Prime Systems Blueprint,Glaive Prime Disc,Ember Prime Neuroptics Blueprint,Latron Prime Receiver,Reaper Prime Blueprint,Silva & Aegis Prime Guard,Kavasa Prime Buckle,Tigris Prime Receiver,Burston Prime Blueprint,Trinity Prime Neuroptics Blueprint,Forma Blueprint,Silva & Aegis Prime Guard,Ballistica Prime Receiver,Sybaris Prime Receiver,Banshee Prime Neuroptics Blueprint,Fang Prime Blade,Helios Prime Carapace,Spira Prime Pouch,Fragor Prime Blueprint,Nikana Prime Blueprint,Bronco Prime Receiver,Hydroid Prime Chassis Blueprint,Forma Blueprint,Silva & Aegis Prime Guard,Destreza Prime Blueprint,Forma Blueprint,Akjagara Prime Blueprint,Bronco Prime Blueprint,Mirage Prime Neuroptics Blueprint,Saryn Prime Chassis Blueprint,Nikana Prime Blade,Spira Prime Blade,Braton Prime Stock,Lex Prime Blueprint,Saryn Prime Systems Blueprint,Stradavar Prime Blueprint,Kronen Prime Handle,Forma Blueprint,Bronco Prime Receiver,Fang Prime Blade,Hydroid Prime Chassis Blueprint,Tigris Prime Blueprint,Banshee Prime Blueprint,Fragor Prime Handle,Burston Prime Blueprint,Dual Kamas Prime Blueprint,Forma Blueprint,Nova Prime Chassis Blueprint,Bronco Prime Barrel,Volt Prime Blueprint,Dual Kamas Prime Blueprint,Nyx Prime Blueprint,Forma Blueprint,Vauban Prime Blueprint,Galatine Prime Handle,Forma Blueprint,Braton Prime Stock,Fang Prime Handle,Galatine Prime Blade,Vauban Prime Neuroptics Blueprint,Tigris Prime Receiver,Forma Blueprint,Carrier Prime Systems,Galatine Prime Blade,Paris Prime String,Venka Prime Gauntlet,Ash Prime Neuroptics Blueprint,Saryn Prime Neuroptics Blueprint,Bronco Prime Receiver,Cernos Prime Grip,Tigris Prime Stock,Vauban Prime Neuroptics Blueprint,Burston Prime Barrel,Forma Blueprint,Helios Prime Carapace,Paris Prime String,Silva & Aegis Prime Hilt,Vauban Prime Chassis Blueprint,Fragor Prime Handle,Mirage Prime Systems Blueprint,Burston Prime Stock,Cernos Prime Grip,Forma Blueprint,Valkyr Prime Chassis Blueprint,Banshee Prime Blueprint,Cernos Prime Blueprint,Akbolto Prime Blueprint,Pyrana Prime Barrel,Forma Blueprint,Volt Prime Neuroptics Blueprint,Bo Prime Handle,Forma Blueprint,Loki Prime Blueprint,Odonata Prime Harness Blueprint,Wyrm Prime Blueprint,Zephyr Prime Blueprint,Galatine Prime Handle,Forma Blueprint,Nekros Prime Chassis Blueprint,Orthos Prime Handle,Paris Prime Blueprint,Zephyr Prime Blueprint,Mesa Prime Blueprint,Pyrana Prime Receiver,Akbolto Prime Blueprint,Braton Prime Stock,Lex Prime Blueprint,Nikana Prime Blueprint,Akstiletto Prime Barrel,Dual Kamas Prime Handle,Braton Prime Stock,Fragor Prime Head,Trinity Prime Systems Blueprint,Aklex Prime Link,Aklex Prime Blueprint,Lex Prime Blueprint,Lex Prime Barrel,Lex Prime Receiver,Forma Blueprint,Akbolto Prime Receiver,Cernos Prime String,Hydroid Prime Neuroptics Blueprint,Braton Prime Barrel,Helios Prime Carapace,Kogake Prime Boot,Akbolto Prime Receiver,Ballistica Prime String,Fang Prime Handle,Braton Prime Stock,Zephyr Prime Neuroptics Blueprint,Forma Blueprint,Akvasto Prime Blueprint,Akvasto Prime Link,Vasto Prime Blueprint,Vasto Prime Barrel,Vasto Prime Receiver,Forma Blueprint,Banshee Prime Systems Blueprint,Cernos Prime Blueprint,Kavasa Prime Buckle,Ash Prime Chassis Blueprint,Fang Prime Blade,Euphona Prime Blueprint,Banshee Prime Systems Blueprint,Orthos Prime Blueprint,Forma Blueprint,Fang Prime Blade,Fragor Prime Head,Sybaris Prime Blueprint,Cernos Prime Lower Limb,Orthos Prime Blueprint,Forma Blueprint,Lex Prime Receiver,Trinity Prime Neuroptics Blueprint,Vectis Prime Barrel,Cernos Prime Lower Limb,Fragor Prime Handle,Silva & Aegis Prime Blade,Braton Prime Blueprint,Trinity Prime Systems Blueprint,Forma Blueprint,Chroma Prime Systems Blueprint,Banshee Prime Blueprint,Pyrana Prime Receiver,Helios Prime Carapace,Hydroid Prime Chassis Blueprint,Forma Blueprint,Chroma Prime Systems Blueprint,Kogake Prime Blueprint,Sybaris Prime Receiver,Braton Prime Blueprint,Hydroid Prime Chassis Blueprint,Pyrana Prime Barrel,Ember Prime Blueprint,Glaive Prime Blade,Forma Blueprint,Frost Prime Neuroptics Blueprint,Latron Prime Barrel,Sicarus Prime Blueprint,Euphona Prime Receiver,Bronco Prime Barrel,Forma Blueprint,Braton Prime Stock,Lex Prime Receiver,Paris Prime Blueprint,Galatine Prime Blueprint,Kavasa Prime Kubrow Collar Blueprint,Forma Blueprint,Nekros Prime Chassis Blueprint,Paris Prime Blueprint,Saryn Prime Systems Blueprint,Helios Prime Cerebrum,Akstiletto Prime Receiver,Trinity Prime Chassis Blueprint,Fragor Prime Head,Vectis Prime Barrel,Forma Blueprint,Helios Prime Cerebrum,Akstiletto Prime Barrel,Trinity Prime Chassis Blueprint,Fang Prime Handle,Lex Prime Receiver,Oberon Prime Chassis Blueprint,Hydroid Prime Systems Blueprint,Burston Prime Barrel,Forma Blueprint,Cernos Prime Grip,Helios Prime Carapace,Kronen Prime Blueprint,Hydroid Prime Systems Blueprint,Akbolto Prime Link,Forma Blueprint,Mesa Prime Chassis Blueprint,Mirage Prime Chassis Blueprint,Paris Prime Upper Limb,Kavasa Prime Buckle,Akstiletto Prime Receiver,Forma Blueprint,Hikou Prime Pouch,Lex Prime Barrel,Odonata Prime Harness Blueprint,Kronen Prime Blade,Cernos Prime String,Galatine Prime Handle,Orthos Prime Handle,Sybaris Prime Blueprint,Forma Blueprint,Kronen Prime Blade,Banshee Prime Blueprint,Pyrana Prime Receiver,Cernos Prime Grip,Paris Prime Blueprint,Tiberon Prime Receiver,Kronen Prime Blade,Destreza Prime Blueprint,Kogake Prime Blueprint,Paris Prime Blueprint,Sybaris Prime Stock,Forma Blueprint,Kronen Prime Blade,Equinox Prime Blueprint,Hydroid Prime Blueprint,Ballistica Prime Lower Limb,Braton Prime Blueprint,Forma Blueprint,Loki Prime Systems Blueprint,Glaive Prime Blade,Forma Blueprint,Latron Prime Blueprint,Reaper Prime Blueprint,Wyrm Prime Carapace,Limbo Prime Neuroptics Blueprint,Tiberon Prime Blueprint,Forma Blueprint,Braton Prime Blueprint,Nami Skyla Prime Blueprint,Orthos Prime Handle,Limbo Prime Chassis Blueprint,Akjagara Prime Receiver,Zephyr Prime Chassis Blueprint,Fang Prime Handle,Kronen Prime Blueprint,Nami Skyla Prime Blueprint,Loki Prime Systems Blueprint,Volt Prime Systems Blueprint,Odonata Prime Blueprint,Bo Prime Ornament,Wyrm Prime Carapace,Forma Blueprint,Ash Prime Systems Blueprint,Akstiletto Prime Link,Odonata Prime Wings Blueprint,Braton Prime Blueprint,Fang Prime Blade,Forma Blueprint,Nikana Prime Hilt,Ash Prime Neuroptics Blueprint,Forma Blueprint,Bronco Prime Blueprint,Carrier Prime Carapace,Lex Prime Receiver,Nekros Prime Blueprint,Bronco Prime Barrel,Volt Prime Blueprint,Dual Kamas Prime Blueprint,Paris Prime Upper Limb,Forma Blueprint,Nikana Prime Hilt,Hydroid Prime Neuroptics Blueprint,Fragor Prime Handle,Cernos Prime Upper Limb,Galatine Prime Blade,Nekros Prime Chassis Blueprint,Nami Skyla Prime Blade,Helios Prime Systems,Nekros Prime Neuroptics Blueprint,Euphona Prime Blueprint,Lex Prime Barrel,Oberon Prime Chassis Blueprint,Nikana Prime Hilt,Braton Prime Receiver,Saryn Prime Blueprint,Braton Prime Barrel,Lex Prime Receiver,Nikana Prime Blueprint,Oberon Prime Systems Blueprint,Akstiletto Prime Receiver,Galatine Prime Handle,Euphona Prime Blueprint,Paris Prime Blueprint,Forma Blueprint,Oberon Prime Systems Blueprint,Helios Prime Systems,Tiberon Prime Blueprint,Fang Prime Blade,Galatine Prime Blade,Paris Prime String,Oberon Prime Systems Blueprint,Venka Prime Blueprint,Zephyr Prime Chassis Blueprint,Burston Prime Stock,Destreza Prime Handle,Forma Blueprint,Oberon Prime Systems Blueprint,Gram Prime Blueprint,Tiberon Prime Blueprint,Destreza Prime Handle,Lex Prime Blueprint,Zephyr Prime Neuroptics Blueprint,Rhino Prime Blueprint,Ankyros Prime Blade,Dakra Prime Blueprint,Boar Prime Receiver,Boltor Prime Stock,Mag Prime Systems Blueprint,Redeemer Prime Handle,Chroma Prime Chassis Blueprint,Mirage Prime Systems Blueprint,Destreza Prime Handle,Lex Prime Receiver,Forma Blueprint,Scindo Prime Blade,Akbronco Prime Link,Trinity Prime Blueprint,Bronco Prime Blueprint,Bronco Prime Receiver,Forma Blueprint,Sicarus Prime Receiver,Ember Prime Systems Blueprint,Forma Blueprint,Latron Prime Receiver,Loki Prime Blueprint,Wyrm Prime Blueprint,Scindo Prime Blade,Ankyros Prime Blade,Nyx Prime Systems Blueprint,Boltor Prime Stock,Hikou Prime Stars,Rhino Prime Systems Blueprint,Soma Prime Stock,Dakra Prime Blade,Nova Prime Blueprint,Boar Prime Receiver,Mag Prime Systems Blueprint,Forma Blueprint,Tigris Prime Blueprint,Akstiletto Prime Receiver,Burston Prime Barrel,Odonata Prime Harness Blueprint,Saryn Prime Systems Blueprint,Vectis Prime Barrel,Tipedo Prime Handle,Burston Prime Barrel,Forma Blueprint,Kogake Prime Boot,Mirage Prime Neuroptics Blueprint,Stradavar Prime Stock,Vauban Prime Chassis Blueprint,Volt Prime Neuroptics Blueprint,Forma Blueprint,Carrier Prime Systems,Dual Kamas Prime Blueprint,Odonata Prime Systems Blueprint,Vectis Prime Stock,Braton Prime Receiver,Boar Prime Barrel,Hikou Prime Pouch,Mag Prime Neuroptics Blueprint,Trinity Prime Systems Blueprint,Vectis Prime Stock,Braton Prime Receiver,Orthos Prime Blueprint,Trinity Prime Neuroptics Blueprint,Trinity Prime Systems Blueprint,Vasto Prime Barrel,Vectis Prime Stock,Cernos Prime Blueprint,Forma Blueprint,Odonata Prime Harness Blueprint,Paris Prime Blueprint,Trinity Prime Systems Blueprint,Valkyr Prime Chassis Blueprint,Braton Prime Receiver,Tigris Prime Receiver,Dual Kamas Prime Blueprint,Venka Prime Blades,Forma Blueprint,Valkyr Prime Chassis Blueprint,Fang Prime Handle,Galatine Prime Handle,Ballistica Prime Lower Limb,Braton Prime Blueprint,Forma Blueprint,Venka Prime Gauntlet,Kogake Prime Blueprint,Silva & Aegis Prime Blueprint,Lex Prime Barrel,Nami Skyla Prime Blueprint,Valkyr Prime Blueprint,Volt Prime Neuroptics Blueprint,Odonata Prime Blueprint,Volt Prime Chassis Blueprint,Lex Prime Receiver,Odonata Prime Systems Blueprint,Forma Blueprint";
        const int textTop = 415;
        const int textHeight = 45;
        WebClient wc = new WebClient();
        TesseractEngine tessEngine;

        public Form1()
        {
            InitializeComponent();
        }

        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.KeyDown += GlobalHookKey_Down;
            m_GlobalHook.KeyUp += GlobalHookKey_Up;
            // m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
        }

        private void GlobalHookKey_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0)
            {
                if (button3.Text == "Shrink")
                {
                    Shrink();
                }
                else
                {
                    Grow();
                }
            }
        }

        private void GlobalHookKey_Up(object sender, KeyEventArgs e)
        {

        }

        public void Unsubscribe()
        {
            // m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyDown -= GlobalHookKey_Down;
            m_GlobalHook.KeyUp -= GlobalHookKey_Up;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Unsubscribe();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tessEngine = new TesseractEngine(Application.StartupPath + @"\tessdata", "eng");
            int dist = LevenshteinDistance.Compute("Prime", "PRIME");
            // Load the items
            LoadItemDB();

            // Set the hotkey
            Subscribe();

            // Go to minimized mode
            // Shrink();
        }


        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // For Debugging
        private void Button1_Click_1(object sender, EventArgs e)
        {
            //int someNum = LevenshteinDistance.Compute("A ,w  Braton Prime Stock", "Braton Prime Stock");
            //MessageBox.Show(someNum.ToString());
            Bitmap lastBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\ReeAppLatest.bmp"));
            Process4Players(lastBitmap);

            // RunTests();
            /*
            Bitmap lastBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\ReeAppLatest.bmp"));
            Process4Players(lastBitmap, false);
            */
            // char[] alphabet = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();
            // MessageBox.Show(new string(alphabet));
            //Bitmap lastBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\ReeAppLatest.bmp"));
            //Process3Players(lastBitmap, true);
            // RunTests();
            // RunTest5();
            // RunTests();
            // Bitmap lastBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 4.bmp"));
            //             Process4Players(lastBitmap);
            // RunTests();
            // 4 Players - 2nd Picture
            /*
            Bitmap lastBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 3.bmp"));
            Process4Players(lastBitmap, true);
            
            Rectangle srcRect = new Rectangle(725, textTop, 240, textHeight);
            Bitmap cropped = (Bitmap)lastBitmap.Clone(srcRect, lastBitmap.PixelFormat);
            cropped.Save(@"C:\Users\Reelix\Pictures\Warframe\Woof\2.bmp");
            pictureBox1.Size = cropped.Size;
            pictureBox1.Image = cropped;
            textBox1.Text = FixCommonOCRErrors(DoOCR(@"C:\Reelix\C# Projects\Libs\Woof\tessdata", @"C:\Users\Reelix\Pictures\Warframe\Woof\2.bmp"));
            */

            /*
            // 4 Players - 3rd Picture
            Bitmap lastBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 3.bmp"));
            Rectangle srcRect = new Rectangle(960, textTop, 240, textHeight);
            Bitmap cropped = (Bitmap)lastBitmap.Clone(srcRect, lastBitmap.PixelFormat);
            cropped.Save(@"C:\Users\Reelix\Pictures\Warframe\Woof\3.bmp");
            pictureBox1.Size = cropped.Size;
            pictureBox1.Image = cropped;*/
        }

        void RunTests()
        {
            DateTime timeBefore = DateTime.Now;
            textBox1.Text = "Running Tests...";
            if (RunTest1() && RunTest2() && RunTest3() && RunTest4() && RunTest5() && RunTest6() && RunTest7() && RunTest8())
            {
                DateTime timeAfter = DateTime.Now;
                TimeSpan timeDiff = timeAfter - timeBefore;
                textBox1.Text = "All Tests Passed in: " + timeDiff.TotalMilliseconds;

            }
        }

        bool RunTest1()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 1.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest2()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 2.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest3()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 3.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest4()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 4.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest5()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 5.png"));
            Process3Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest6()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 6.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest7()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 7.bmp"));
            Process4Players(testBitmap, true);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        bool RunTest8()
        {
            Bitmap testBitmap = new Bitmap(Image.FromFile(@"C:\Users\Reelix\Pictures\Warframe\Tests\Test 8.bmp"));
            Process4Players(testBitmap, false);
            if (textBox1.Text.Contains("Unknown"))
            {
                return false;
            }
            return true;
        }

        private Bitmap DoSetup()
        {
            textBox1.Text = "Loading...";
            Process p = Process.GetProcessesByName("Warframe.x64").FirstOrDefault();
            if (p == null)
            {
                textBox1.Text = "Warframe needs to be running...";
                // MessageBox.Show("Warframe needs to be running...");
                return null;
            }

            FocusWindow(p.Id);
            Thread.Sleep(50); // Give it time to focus
            Process thisApp = Process.GetCurrentProcess();
            Bitmap warframeScreenshot = CaptureWindow(p.MainWindowHandle);
            FocusWindow(thisApp.Id);
            warframeScreenshot.Save($@"C:\Users\{username}\Pictures\Warframe\ReeAppLatest.bmp");
            if (warframeScreenshot.Width != 1920 || warframeScreenshot.Height != 1080)
            {
                textBox1.Text = "Error: Cannot capture Warframe window" + Environment.NewLine + "Make sure Warframe is set to Display Mode: Borderless Fullscreen";
                return null;
            }
            return warframeScreenshot;
        }

        private void btn3Players_Click(object sender, EventArgs e)
        {
            // 3 Players
            Bitmap warframeScreenshot = DoSetup();
            if (warframeScreenshot != null)
            {
                Process3Players(warframeScreenshot);
            }
        }

        private void btn4Players_Click(object sender, EventArgs e)
        {
            // 3 Players
            Bitmap warframeScreenshot = DoSetup();
            if (warframeScreenshot != null)
            {
                Process4Players(warframeScreenshot);
            }
        }

        void Process3Players(Bitmap theBitmap, bool debug = false)
        {
            textBox1.Text = "Loading...";
            this.Refresh();
            if (!debug)
            {
                textBox1.Text = "";
            }
            // Split into the 3 items - Assuming 3 players

            // Item 1

            int item = 1;
            foreach (int xPos in new int[] { 595, 840, 1080 }) // 1920x1080
            {
                Rectangle srcRect = new Rectangle(xPos, textTop, 240, textHeight);
                Bitmap cropped = (Bitmap)theBitmap.Clone(srcRect, theBitmap.PixelFormat);

                cropped.Save($@"C:\Users\{username}\Pictures\Warframe\Woof\{item}.bmp");
                string ocrResult = DoOCR(Application.StartupPath + @"\tessdata", $@"C:\Users\{username}\Pictures\Warframe\Woof\{item}.bmp");
                ocrResult = FixCommonOCRErrors(ocrResult);
                string foundItem = findItem(ocrResult);
                if (foundItem != "")
                {
                    if (debug)
                    {
                        textBox1.Text += foundItem + Environment.NewLine;
                    }
                    else
                    {
                        var itemCost = getItemCost(foundItem);
                        textBox1.Text += $"Item {item}: {foundItem} - Value: {itemCost.lowValue} to {itemCost.highValue} {Environment.NewLine}";
                    }
                }
                else
                {
                    textBox1.Text += $"Item {item}: Unknown --> {ocrResult} {Environment.NewLine}";
                }
            }
        }

        void Process4Players(Bitmap theBitmap, bool debug = false)
        {
            textBox1.Text = "Loading...";
            textBox1.Refresh();
            if (!debug)
            {
                textBox1.Text = "";
            }
            // Split into the 4 items - Assuming 4 players

            int item = 1;
            foreach (int xPos in new int[] { 475, 725, 960, 1205 }) // 1920x1080
            {
                Rectangle srcRect = new Rectangle(xPos, textTop, 240, textHeight);
                Bitmap cropped = (Bitmap)theBitmap.Clone(srcRect, theBitmap.PixelFormat);

                cropped.Save($@"C:\Users\{username}\Pictures\Warframe\Woof\{item}.bmp");
                string ocrResult = DoOCR(Application.StartupPath + @"\tessdata", $@"C:\Users\{username}\Pictures\Warframe\Woof\{item}.bmp");
                ocrResult = FixCommonOCRErrors(ocrResult);
                string foundItem = findItem(ocrResult);
                if (foundItem != "")
                {
                    if (debug)
                    {
                        textBox1.Text += foundItem + Environment.NewLine;
                    }
                    else
                    {
                        var itemCost = getItemCost(foundItem);
                        textBox1.Text += $"Item {item}: {foundItem} - Value: {itemCost.lowValue} to {itemCost.highValue} {Environment.NewLine}";
                        textBox1.Refresh();
                    }
                }
                else
                {
                    textBox1.Text += $"Item {item}: Unknown --> {ocrResult} {Environment.NewLine}";
                }
                item++;
            }
        }

        private string DoOCR(string tessdataPath, string filePath)
        {
            string returnText = "";
            tessEngine.SetVariable("tessedit_char_whitelist", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ &"); // Only regular letters

            string theVersion = tessEngine.Version;
            using (var img = Pix.LoadFromFile(filePath))
            {
                using (var page = tessEngine.Process(img))
                {
                    returnText = page.GetText();
                }
            }
            return returnText;
        }

        private string FixCommonOCRErrors(string text)
        {
            text = text.Replace("Prine", "Prime");
            text = text.Replace("Primestock", "Prime Stock");
            text = text.Replace("\n", " ");
            text = text.Trim();
            return text;
        }

        private string findItem(string itemName)
        {
            // Make it API Friendly
            if (itemName.Contains("Neuroptics") || itemName.Contains("Systems") || itemName.Contains("Chassis"))
            {
                itemName = itemName.Replace(" Blueprint", "");
            }

            // First, see if it exists directly
            foreach (string item in warframeItems.Split(','))
            {
                if (itemName.ToLower().Contains(item.ToLower()))
                {
                    return item;
                }
            }

            // Ok - It doesn't exist directly - Hard Mode!
            Dictionary<int, string> lDic = new Dictionary<int, string>();
            int lowestLevNum = 10;
            string levItem = "";
            foreach (string item in warframeItems.Split(','))
            {
                int levNum = LevenshteinDistance.Compute(itemName, item);
                if (levNum < lowestLevNum)
                {
                    lowestLevNum = levNum;
                    levItem = item;
                }
            }
            if (lowestLevNum <= 5) // Close enough
            {
                return levItem;
            }
            return "";
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private void FocusWindow(int processId)
        {
            Process p = Process.GetProcessById(processId);
            if (p != null)
            {
                SetForegroundWindow(p.MainWindowHandle);
            }
        }

        // https://api.warframe.market/v1/items/akbolto_prime_receiver/orders
        private (int lowValue, int highValue) getItemCost(string itemName)
        {
            // Make it API Friendly
            if (itemName.Contains("Neuroptics") || itemName.Contains("Systems") || itemName.Contains("Chassis"))
            {
                itemName = itemName.Replace(" Blueprint", "");
            }
            // The real name is Kavasa Prime X, but warframe.market calls it Kavasa Prime Collar X
            itemName = itemName.Replace("Kavasa Prime Buckle", "Kavasa Prime Collar Buckle");
            itemName = itemName.Replace("Kavasa Prime Band", "Kavasa Prime Collar Band");

            // API deals in lower case
            // API uses _'s as spaces
            // API uses "and" instead of "&"
            itemName = itemName.ToLower().Replace(" ", "_").Replace("&", "and");

            // Forma are special - This can be sped up
            if (itemName == "forma_blueprint")
            {
                return (5, 5);
            }

            string urlString = "https://api.warframe.market/v1/items/" + itemName + "/orders";
            string rawItemData = wc.DownloadString(urlString);

            Rootobject itemData = new Rootobject();
            try
            {
                itemData = JsonConvert.DeserializeObject<Rootobject>(rawItemData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ReeError: {ex}");
                return (999, 999);
            }
            // Only users that are active (Last online in the past 7 days)
            List<Order> orders = itemData.payload.orders.Where(x => x.user.Last_seen > (DateTime.Now.AddDays(-7))).ToList();
            var something = orders.Any();

            // Only en users on pc (ru / console have weird prices)
            orders = orders.Where(x => x.platform == "pc" && x.Region == "en").ToList();

            // And only orders that were semi-recently placed
            orders = orders.Where(x => x.Creation_date > (DateTime.Now.AddDays(-21))).ToList();

            // Get the Highest Buy Order
            List<Order> buyOrders = orders.Where(x => x.Order_type == "buy").ToList();

            Order HighestBuyOrder = buyOrders.OrderByDescending(x => x.Platinum).FirstOrDefault();

            if (HighestBuyOrder == null)
            {
                HighestBuyOrder = new Order();
            }

            // Get the Lowest Sell Order
            List<Order> sellOrders = orders.Where(x => x.Order_type == "sell").ToList();
            Order LowestSellOrder = sellOrders.OrderBy(x => x.Platinum).First();

            // Nice return values
            if (HighestBuyOrder.Platinum > LowestSellOrder.Platinum)
            {
                return ((int)LowestSellOrder.Platinum, (int)HighestBuyOrder.Platinum);
            }
            else
            {
                return ((int)HighestBuyOrder.Platinum, (int)LowestSellOrder.Platinum);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Shrink")
            {
                Shrink();
            }
            else
            {
                Grow();
            }
        }

        private void Shrink()
        {
            Process p = Process.GetProcessesByName("Warframe.x64").FirstOrDefault();
            if (p == null)
            {
                textBox1.Text = "Warframe needs to be running...";
                Grow();
            }
            else
            {
                button3.Text = "Grow";
                this.Size = new Size(75, 26);
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 75, Screen.PrimaryScreen.Bounds.Height - 26);
                FocusWindow(p.Id);
            }
        }

        private void Grow()
        {
            button3.Text = "Shrink";
            this.Size = new Size(646, 374);
            this.Location = new Point(500, 500);
            FocusWindow(Process.GetCurrentProcess().Id);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string toCopy = textBox1.Text;
            List<string> items = toCopy.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            // Item 1: Lex Prime Barrel - Value: 1 to 2
            string finalText = "";
            foreach (string item in items)
            {
                string itemName = item.Remove(0, item.IndexOf(":") + 1);
                itemName = itemName.Substring(0, itemName.IndexOf("-") - 1);
                string itemFromPrice = item.Remove(0, item.IndexOf("Value: ") + 7);
                itemFromPrice = itemFromPrice.Substring(0, itemFromPrice.IndexOf(" "));
                string itemToPrice = item.Remove(0, item.IndexOf("Value: "));
                itemToPrice = itemToPrice.Remove(0, itemToPrice.IndexOf("to ") + 3);

                finalText += $"{itemName} ({itemFromPrice}:platinum:->{itemToPrice}:platinum:) ";
            }
            finalText += "- Auto-Generated By: Reelix";
            Clipboard.SetText(finalText);

            // And Shrink
            Shrink();
        }

        private void BtnUpdateDatabase_Click(object sender, EventArgs e)
        {
            lblDatabase.Text = "Database: ???";
            textBox1.Text = "Updating...";
            this.Refresh();
            string rawItemData = wc.DownloadString("https://api.warframe.market/v1/items");
            ItemDataRoot itemDataRoot = new ItemDataRoot();
            try
            {
                ItemDataRoot parsedItemData = JsonConvert.DeserializeObject<ItemDataRoot>(rawItemData);
                List<string> items = parsedItemData.payload.items.Select(x => x.item_name).ToList();
                items = items.Where(x => x.Contains(" Prime")).ToList(); // Only prime items can drop from relics (And Forma, but market doesn't have Forma)
                items = items.Where(x => !x.Contains(" Set")).ToList(); // Prime Sets don't count
                string longItemList = string.Join(",", items);
                StreamWriter sw1 = new StreamWriter(Application.StartupPath + @"\Database.txt");
                sw1.WriteLine(longItemList);
                sw1.Flush();
                sw1.Close();
                LoadItemDB();
                this.TopMost = false;
                textBox1.Text = "";
                this.Refresh();
                MessageBox.Show("Database Updated!");
                this.TopMost = true;
            }
            catch
            {
                this.TopMost = false;
                MessageBox.Show("Unable to update the DB :(");
                this.TopMost = true;
            }
        }

        private void LoadItemDB()
        {
            int itemCount = 0;
            if (!File.Exists(Application.StartupPath + @"\Database.txt"))
            {
                lblDatabase.Text = "Database: N/A";
            }
            else
            {
                StreamReader sr1 = new StreamReader(Application.StartupPath + @"\Database.txt");
                warframeItems = sr1.ReadLine();
                sr1.Close();
                StringBuilder sb1 = new StringBuilder(warframeItems);
                sb1.Append(",Forma Blueprint"); // A valid Relic item warframe.market doesn't have
                warframeItems = sb1.ToString();
                itemCount = warframeItems.Split(',').Length;
                lblDatabase.Text = "Database: " + itemCount;
            }
        }
    }
}
