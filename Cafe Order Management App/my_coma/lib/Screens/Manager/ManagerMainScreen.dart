import 'package:flutter/material.dart';
import 'package:my_coma/Screens/Manager/Products/ProductsPage.dart';
import 'package:my_coma/Screens/Manager/Reports/MonthReport.dart';
import 'package:my_coma/Screens/Manager/Reports/YearlyReport.dart';
import 'package:my_coma/Screens/Manager/Tables/TablesPage.dart';
import 'package:my_coma/Screens/Manager/Users/Users.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:my_coma/Screens/Manager/Reports/TodayReport.dart';
import 'package:my_coma/generated/l10n.dart';


class ManagerMainScreen extends StatefulWidget {
  const ManagerMainScreen({super.key});

  @override
  // ignore: library_private_types_in_public_api
  _ManagerMainScreenState createState() => _ManagerMainScreenState();
}

class _ManagerMainScreenState extends State<ManagerMainScreen> {
  late String selectedSection = S.of(context).scnDH;
  String managerName = ''; // Kullanıcı adını saklamak için bir değişken

  @override
  void initState() {
    super.initState();
    _loadManagerName(); // Adı yüklemek için fonksiyonu çağırıyoruz
  }

  Future<void> _loadManagerName() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    setState(() {
      managerName = prefs.getString('name') ?? 'Manager'; // Adı yükle ve varsayılan olarak 'Manager' ayarla
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
          '${managerName[0].toUpperCase() + managerName.substring(1).toLowerCase()} $selectedSection',
          style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 24, color: Colors.white),
        ),
        backgroundColor: Colors.indigo.shade900,
        iconTheme: const IconThemeData(color: Colors.white),
      ),
      drawer: Drawer(
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              buildHeader(context),
              buildMenuItems(context),
            ],
          ),
        ),
      ),
      body: GridView.count(
  padding: const EdgeInsets.all(16),
  crossAxisCount: 2,
  crossAxisSpacing: 16,
  mainAxisSpacing: 16,
  children: [
    buildDashboardCard(Icons.today, S.of(context).scnDD, () {
      Navigator.of(context).push(MaterialPageRoute(builder: (context) => const TodayReportPage()));
    }),
    buildDashboardCard(Icons.list_alt, S.of(context).scnDP, () {
      Navigator.of(context).push(MaterialPageRoute(builder: (context) => const ProductsPage()));
    }),
    buildDashboardCard(Icons.person_outline, S.of(context).scnDU, () {
      Navigator.of(context).push(MaterialPageRoute(builder: (context) => const UsersPage()));
    }),
    buildDashboardCard(Icons.table_chart, S.of(context).scnDT, () {
      Navigator.of(context).push(MaterialPageRoute(builder: (context) => const TablesPage()));
    }),
  ],
),

    );
  }

  Widget buildDashboardCard(IconData icon, String title, VoidCallback onTap) {
  return GestureDetector(
    onTap: onTap,
    child: Card(
      elevation: 4,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(icon, size: 48, color: Colors.indigo.shade900),
          const SizedBox(height: 8),
          Text(title, style: TextStyle(fontSize: 16, fontWeight: FontWeight.w600, color: Colors.indigo.shade900)),
        ],
      ),
    ),
  );
}

 Widget buildHeader(BuildContext context) => Container(
      color: Colors.indigo.shade900,
      height: 100, // Sabit bir yükseklik belirleyerek ortalamayı kolaylaştırır
      child: const Center(
        child: Text(
          "COMA",
          style: TextStyle(
            fontSize: 24,
            fontWeight: FontWeight.bold,
            color: Colors.white,
          ),
        ),
      ),
    );


  Widget buildMenuItems(BuildContext context) => Container(
    padding: const EdgeInsets.all(16),
    child: Column(
      children: [
        buildMenuItem(context, Icons.today, S.of(context).scnDD, () {
          Navigator.pop(context);
          Navigator.of(context).push(MaterialPageRoute(builder: (context) => const TodayReportPage()));
        }),
        buildMenuItem(context, Icons.calendar_today, S.of(context).scnDM, () {
          Navigator.pop(context);
          Navigator.of(context).push(MaterialPageRoute(builder: (context) => const MonthlyReportPage()));
        }),
        buildMenuItem(context, Icons.calendar_today_outlined, S.of(context).scnDY, () {
          Navigator.pop(context);
          Navigator.of(context).push(MaterialPageRoute(builder: (context) => const YearlyReportPage()));
        }),
        buildDivider(),
        buildMenuItem(context, Icons.person_outline, S.of(context).scnDU, () {
          Navigator.pop(context);
          Navigator.of(context).push(MaterialPageRoute(builder: (context) => const UsersPage()));
        }),
        buildDivider(),
        buildMenuItem(context, Icons.list_alt, S.of(context).scnDP, () {
          Navigator.pop(context);
          Navigator.of(context).push(MaterialPageRoute(builder: (context) => const ProductsPage()));
        }),
        buildDivider(),
        buildMenuItem(context, Icons.table_chart, S.of(context).scnDT, () {
          Navigator.pop(context);
          Navigator.of(context).push(MaterialPageRoute(builder: (context) => const TablesPage()));
        }),
      ],
    ),
  );

  Widget buildMenuItem(BuildContext context, IconData icon, String title, VoidCallback onTap) {
    return ListTile(
      leading: Icon(icon, color: Colors.indigo.shade900),
      title: Text(
        title,
        style: TextStyle(fontWeight: FontWeight.w500, color: Colors.indigo.shade900),
      ),
      onTap: onTap,
    );
  }

  Widget buildDivider() => const Divider(
    color: Colors.black54,
  );
}
