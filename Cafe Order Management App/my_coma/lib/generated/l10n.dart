// GENERATED CODE - DO NOT MODIFY BY HAND
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'intl/messages_all.dart';

// **************************************************************************
// Generator: Flutter Intl IDE plugin
// Made by Localizely
// **************************************************************************

// ignore_for_file: non_constant_identifier_names, lines_longer_than_80_chars
// ignore_for_file: join_return_with_assignment, prefer_final_in_for_each
// ignore_for_file: avoid_redundant_argument_values, avoid_escaping_inner_quotes

class S {
  S();

  static S? _current;

  static S get current {
    assert(_current != null,
        'No instance of S was loaded. Try to initialize the S delegate before accessing S.current.');
    return _current!;
  }

  static const AppLocalizationDelegate delegate = AppLocalizationDelegate();

  static Future<S> load(Locale locale) {
    final name = (locale.countryCode?.isEmpty ?? false)
        ? locale.languageCode
        : locale.toString();
    final localeName = Intl.canonicalizedLocale(name);
    return initializeMessages(localeName).then((_) {
      Intl.defaultLocale = localeName;
      final instance = S();
      S._current = instance;

      return instance;
    });
  }

  static S of(BuildContext context) {
    final instance = S.maybeOf(context);
    assert(instance != null,
        'No instance of S present in the widget tree. Did you add S.delegate in localizationsDelegates?');
    return instance!;
  }

  static S? maybeOf(BuildContext context) {
    return Localizations.of<S>(context, S);
  }

  /// `Login`
  String get scnLTitle {
    return Intl.message(
      'Login',
      name: 'scnLTitle',
      desc: '',
      args: [],
    );
  }

  /// `Username`
  String get scnLU {
    return Intl.message(
      'Username',
      name: 'scnLU',
      desc: '',
      args: [],
    );
  }

  /// `Password`
  String get scnLP {
    return Intl.message(
      'Password',
      name: 'scnLP',
      desc: '',
      args: [],
    );
  }

  /// `Invalid username or password`
  String get scnLE {
    return Intl.message(
      'Invalid username or password',
      name: 'scnLE',
      desc: '',
      args: [],
    );
  }

  /// `User not found`
  String get scnLN {
    return Intl.message(
      'User not found',
      name: 'scnLN',
      desc: '',
      args: [],
    );
  }

  /// `An unexpected error occurred`
  String get scnLA {
    return Intl.message(
      'An unexpected error occurred',
      name: 'scnLA',
      desc: '',
      args: [],
    );
  }

  /// `Check your internet connection`
  String get scnLC {
    return Intl.message(
      'Check your internet connection',
      name: 'scnLC',
      desc: '',
      args: [],
    );
  }

  /// `Dashboard`
  String get scnDH {
    return Intl.message(
      'Dashboard',
      name: 'scnDH',
      desc: '',
      args: [],
    );
  }

  /// `Daily Report`
  String get scnDD {
    return Intl.message(
      'Daily Report',
      name: 'scnDD',
      desc: '',
      args: [],
    );
  }

  /// `Total ₺`
  String get scnDDT {
    return Intl.message(
      'Total ₺',
      name: 'scnDDT',
      desc: '',
      args: [],
    );
  }

  /// `Still No Sale`
  String get scnDDS {
    return Intl.message(
      'Still No Sale',
      name: 'scnDDS',
      desc: '',
      args: [],
    );
  }

  /// `Loading...`
  String get scnDL {
    return Intl.message(
      'Loading...',
      name: 'scnDL',
      desc: '',
      args: [],
    );
  }

  /// `Monthly Report`
  String get scnDM {
    return Intl.message(
      'Monthly Report',
      name: 'scnDM',
      desc: '',
      args: [],
    );
  }

  /// `Daily income(₺)`
  String get scnDMDI {
    return Intl.message(
      'Daily income(₺)',
      name: 'scnDMDI',
      desc: '',
      args: [],
    );
  }

  /// `Total Yearly income(₺)`
  String get scnDMYI {
    return Intl.message(
      'Total Yearly income(₺)',
      name: 'scnDMYI',
      desc: '',
      args: [],
    );
  }

  /// `Day`
  String get scnDMDD {
    return Intl.message(
      'Day',
      name: 'scnDMDD',
      desc: '',
      args: [],
    );
  }

  /// `Yearly Report`
  String get scnDY {
    return Intl.message(
      'Yearly Report',
      name: 'scnDY',
      desc: '',
      args: [],
    );
  }

  /// `Total Monthly income(₺) : `
  String get scnDMMI {
    return Intl.message(
      'Total Monthly income(₺) : ',
      name: 'scnDMMI',
      desc: '',
      args: [],
    );
  }

  /// `Month`
  String get scnDMDM {
    return Intl.message(
      'Month',
      name: 'scnDMDM',
      desc: '',
      args: [],
    );
  }

  /// `Users`
  String get scnDU {
    return Intl.message(
      'Users',
      name: 'scnDU',
      desc: '',
      args: [],
    );
  }

  /// `Search user...`
  String get scnDUS {
    return Intl.message(
      'Search user...',
      name: 'scnDUS',
      desc: '',
      args: [],
    );
  }

  /// `Add user`
  String get scnDUA {
    return Intl.message(
      'Add user',
      name: 'scnDUA',
      desc: '',
      args: [],
    );
  }

  /// `Update user`
  String get scnDUU {
    return Intl.message(
      'Update user',
      name: 'scnDUU',
      desc: '',
      args: [],
    );
  }

  /// `Username`
  String get scnDUN {
    return Intl.message(
      'Username',
      name: 'scnDUN',
      desc: '',
      args: [],
    );
  }

  /// `Password`
  String get scnDUP {
    return Intl.message(
      'Password',
      name: 'scnDUP',
      desc: '',
      args: [],
    );
  }

  /// `Role`
  String get scnDUR {
    return Intl.message(
      'Role',
      name: 'scnDUR',
      desc: '',
      args: [],
    );
  }

  /// `Users Failed to Load`
  String get scnDUE {
    return Intl.message(
      'Users Failed to Load',
      name: 'scnDUE',
      desc: '',
      args: [],
    );
  }

  /// `Successfully deleted`
  String get scnDUD {
    return Intl.message(
      'Successfully deleted',
      name: 'scnDUD',
      desc: '',
      args: [],
    );
  }

  /// `Could Not Be deleted`
  String get scnDE {
    return Intl.message(
      'Could Not Be deleted',
      name: 'scnDE',
      desc: '',
      args: [],
    );
  }

  /// `User Could Not Be Deleted`
  String get scnDUF {
    return Intl.message(
      'User Could Not Be Deleted',
      name: 'scnDUF',
      desc: '',
      args: [],
    );
  }

  /// `User updated successfully`
  String get scnDUUS {
    return Intl.message(
      'User updated successfully',
      name: 'scnDUUS',
      desc: '',
      args: [],
    );
  }

  /// `User Added successfully`
  String get scnDUAS {
    return Intl.message(
      'User Added successfully',
      name: 'scnDUAS',
      desc: '',
      args: [],
    );
  }

  /// `User Could Not Be Updated`
  String get scnDUUF {
    return Intl.message(
      'User Could Not Be Updated',
      name: 'scnDUUF',
      desc: '',
      args: [],
    );
  }

  /// `User Could Not Be Added`
  String get scnDUAF {
    return Intl.message(
      'User Could Not Be Added',
      name: 'scnDUAF',
      desc: '',
      args: [],
    );
  }

  /// `Please Enter Name`
  String get scnDUEE {
    return Intl.message(
      'Please Enter Name',
      name: 'scnDUEE',
      desc: '',
      args: [],
    );
  }

  /// `Please Enter Password`
  String get scnDUEPE {
    return Intl.message(
      'Please Enter Password',
      name: 'scnDUEPE',
      desc: '',
      args: [],
    );
  }

  /// `Please Enter Price`
  String get scnDUEPP {
    return Intl.message(
      'Please Enter Price',
      name: 'scnDUEPP',
      desc: '',
      args: [],
    );
  }

  /// `Please Enter Valid Number`
  String get scnDVN {
    return Intl.message(
      'Please Enter Valid Number',
      name: 'scnDVN',
      desc: '',
      args: [],
    );
  }

  /// `Price must be greater than '0'`
  String get scnDVNE {
    return Intl.message(
      'Price must be greater than \'0\'',
      name: 'scnDVNE',
      desc: '',
      args: [],
    );
  }

  /// `Products`
  String get scnDP {
    return Intl.message(
      'Products',
      name: 'scnDP',
      desc: '',
      args: [],
    );
  }

  /// `Search product...`
  String get scnDPS {
    return Intl.message(
      'Search product...',
      name: 'scnDPS',
      desc: '',
      args: [],
    );
  }

  /// `Add product`
  String get scnDPA {
    return Intl.message(
      'Add product',
      name: 'scnDPA',
      desc: '',
      args: [],
    );
  }

  /// `Update product`
  String get scnDPU {
    return Intl.message(
      'Update product',
      name: 'scnDPU',
      desc: '',
      args: [],
    );
  }

  /// `Name`
  String get scnDPN {
    return Intl.message(
      'Name',
      name: 'scnDPN',
      desc: '',
      args: [],
    );
  }

  /// `Price`
  String get scnDPP {
    return Intl.message(
      'Price',
      name: 'scnDPP',
      desc: '',
      args: [],
    );
  }

  /// `Status`
  String get scnDPPS {
    return Intl.message(
      'Status',
      name: 'scnDPPS',
      desc: '',
      args: [],
    );
  }

  /// `Products Failed to Load`
  String get scnDPE {
    return Intl.message(
      'Products Failed to Load',
      name: 'scnDPE',
      desc: '',
      args: [],
    );
  }

  /// `Product Added Successfully`
  String get scnDPAS {
    return Intl.message(
      'Product Added Successfully',
      name: 'scnDPAS',
      desc: '',
      args: [],
    );
  }

  /// `Product Could Not Be Added`
  String get scnDPAE {
    return Intl.message(
      'Product Could Not Be Added',
      name: 'scnDPAE',
      desc: '',
      args: [],
    );
  }

  /// `Product updated successfully`
  String get scnDPUM {
    return Intl.message(
      'Product updated successfully',
      name: 'scnDPUM',
      desc: '',
      args: [],
    );
  }

  /// `Product could not be updated`
  String get scnDPME {
    return Intl.message(
      'Product could not be updated',
      name: 'scnDPME',
      desc: '',
      args: [],
    );
  }

  /// `Tables`
  String get scnDT {
    return Intl.message(
      'Tables',
      name: 'scnDT',
      desc: '',
      args: [],
    );
  }

  /// `Search table...`
  String get scnDTS {
    return Intl.message(
      'Search table...',
      name: 'scnDTS',
      desc: '',
      args: [],
    );
  }

  /// `Add table`
  String get scnDTA {
    return Intl.message(
      'Add table',
      name: 'scnDTA',
      desc: '',
      args: [],
    );
  }

  /// `Update table`
  String get scnDTU {
    return Intl.message(
      'Update table',
      name: 'scnDTU',
      desc: '',
      args: [],
    );
  }

  /// `Table No`
  String get scnDTN {
    return Intl.message(
      'Table No',
      name: 'scnDTN',
      desc: '',
      args: [],
    );
  }

  /// `Tables Failed to Load`
  String get scnDTF {
    return Intl.message(
      'Tables Failed to Load',
      name: 'scnDTF',
      desc: '',
      args: [],
    );
  }

  /// `Table added successfully`
  String get scnDTAM {
    return Intl.message(
      'Table added successfully',
      name: 'scnDTAM',
      desc: '',
      args: [],
    );
  }

  /// `Could not add table`
  String get scnDTAF {
    return Intl.message(
      'Could not add table',
      name: 'scnDTAF',
      desc: '',
      args: [],
    );
  }

  /// `Please Enter No`
  String get scnDTASM {
    return Intl.message(
      'Please Enter No',
      name: 'scnDTASM',
      desc: '',
      args: [],
    );
  }

  /// `Table Updated Successfully`
  String get scnDTUM {
    return Intl.message(
      'Table Updated Successfully',
      name: 'scnDTUM',
      desc: '',
      args: [],
    );
  }

  /// `Table Could not be Updated`
  String get scnDTUE {
    return Intl.message(
      'Table Could not be Updated',
      name: 'scnDTUE',
      desc: '',
      args: [],
    );
  }
}

class AppLocalizationDelegate extends LocalizationsDelegate<S> {
  const AppLocalizationDelegate();

  List<Locale> get supportedLocales {
    return const <Locale>[
      Locale.fromSubtags(languageCode: 'en'),
      Locale.fromSubtags(languageCode: 'tr'),
    ];
  }

  @override
  bool isSupported(Locale locale) => _isSupported(locale);
  @override
  Future<S> load(Locale locale) => S.load(locale);
  @override
  bool shouldReload(AppLocalizationDelegate old) => false;

  bool _isSupported(Locale locale) {
    for (var supportedLocale in supportedLocales) {
      if (supportedLocale.languageCode == locale.languageCode) {
        return true;
      }
    }
    return false;
  }
}
